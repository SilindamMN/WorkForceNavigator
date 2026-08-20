namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Numerics;
    using System.Threading.Tasks;
    using Application.Helpers;
    using Application.Interfaces;
    using AutoMapper;
    using Domain.Dtos.General;
    using Domain.Enties;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto>
      where TEntity : class
      where TDto : class
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public GenericService(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GeneralServiceResponseDto> CreateAsync(TDto entityDto)
        {
            try
            {
                var newEntity = mapper.Map<TEntity>(entityDto);
                dataContext.Set<TEntity>().Add(newEntity);
                await SaveAsync();

                var createdDto = mapper.Map<TDto>(newEntity);
                return ResponseHelper.CreateResponse(true, 200, "Created Successfully");
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "Failed to create entity");
            }
        }

        public async Task<GeneralServiceResponseDto> UpdateAsync(int id, TDto updatedEntityDto)
        {
            try
            {
                var getByIdResult = await GetByIdAsync(id);

                if (getByIdResult is null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, $"Entity with id {id} not found.");
                }

                var oldEntity = await dataContext.Set<TEntity>().FindAsync(id);

                if (oldEntity == null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, $"Entity with id {id} not found.");
                }

                mapper.Map(updatedEntityDto, oldEntity, opts =>
                    opts.Items["IgnoreNulls"] = true);

                await SaveAsync();

                var updatedDto = mapper.Map<TDto>(oldEntity);
                return ResponseHelper.CreateResponse(true, 200, "Updated successfully.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status409Conflict, $"Concurrency conflict updating id {id}: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, $"Database error updating id {id}: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status500InternalServerError, $"Failed to update id {id}: {ex.Message}");
            }
        }

        public async Task<GeneralServiceResponseDto> SoftDeleteAsync(int id)
        {
            try
            {
                var entity = await dataContext.Set<TEntity>().FindAsync(id);

                if (entity == null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "Not found  for ${Id}");
                }

                // Soft delete by setting the 'IsDeleted' property to true
                entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);

                // Update the 'LastModified' property
                entity.GetType().GetProperty("LastModified")?.SetValue(entity, DateTime.Now);

                await dataContext.SaveChangesAsync();

                return ResponseHelper.CreateResponse(true, 200, "Successfully");
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "Failed for ${Id}");
            }
        }

        public async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await dataContext.Set<TEntity>()
                .Where(e => EF.Property<int>(e, "Id") == id && EF.Property<bool>(e, "IsDeleted") == false)
                .FirstOrDefaultAsync();

            if (entity == null)
                return default;

            return mapper.Map<TDto>(entity);
        }
        private async Task SaveAsync()
        {
            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Failed to save changes.", ex);
            }
        }

        public async Task<GeneralServiceResponseDto> UndoSoftDeleteAsync(int id)
        {
            try
            {
                var entity = await dataContext.Set<TEntity>().FindAsync(id);

                if (entity == null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "Not found  for ${Id}");
                }

                // Soft delete by setting the 'IsDeleted' property to true
                entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, false);

                // Update the 'LastModified' property
                entity.GetType().GetProperty("LastModified")?.SetValue(entity, DateTime.Now);

                await dataContext.SaveChangesAsync();

                return ResponseHelper.CreateResponse(true, 200, "Successfully");
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "Failed for ${Id}");
            }
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            try
            {
                var entities = dataContext.Set<TEntity>()
                    .Where(e => EF.Property<bool>(e, "IsDeleted") == false)
                    .ToList();

                var dtos = mapper.Map<List<TDto>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return new List<TDto>();
            }
        }

        public async Task<IEnumerable<TEntity>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return dataContext.Set<TEntity>()
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
    }
}