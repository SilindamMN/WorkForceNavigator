namespace Application.Interfaces.GenericInterfaces
{
  using Domain.Dtos.General;
  using Domain.Enties;

  public interface IGenericService<TEntity, TDto>
     where TEntity : class
     where TDto : class
  {
    Task <GeneralServiceResponseDto> CreateAsync(TDto entityDto);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task <GeneralServiceResponseDto>UpdateAsync(int id, TDto updatedEntityDto);
    Task<GeneralServiceResponseDto> SoftDelete(int id);
    Task<TDto> GetByIdAsync(int id);
    Task<GeneralServiceResponseDto> UndoSoftDeleteAsync(int id); // New method
    Task<IEnumerable<TEntity>> GetPagedAsync(int pageNumber, int pageSize);
  }
}