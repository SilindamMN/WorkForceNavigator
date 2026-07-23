namespace Application.Services.Auth
{
    using Application.Dtos.Account.Logs;
    using Application.Helpers;
    using Application.Interfaces.Auth;
    using AutoMapper;
    using Domain.Dtos.General;
    using Domain.Enties.Hr;
    using Domain.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class LogService : ILogService
  {
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public LogService(DataContext dataContext,IMapper mapper)
    {
      this.dataContext = dataContext;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<GetLogDto>> getLogsAsync()
    {
      var logs = await dataContext.Logs.OrderByDescending(x => x.CreatedAt).ToListAsync();
      return mapper.Map<IEnumerable<GetLogDto>>(logs);
    }

    public async Task<IEnumerable<GetLogDto>> getMyLogsAsync(ClaimsPrincipal user)
    {
      var logs = await dataContext.Logs.Where(u=>u.Username==user.Identity.Name).OrderByDescending(x => x.CreatedAt).ToListAsync();
      return mapper.Map<IEnumerable<GetLogDto>>(logs);
    }

    public async Task<GeneralServiceResponseDto> SaveNewLogAsync(string userName, string description)
    {
      var newLog = new Log()
      {
        Username = userName,
        Description = description
      };
      await dataContext.Logs.AddAsync(newLog);
      await dataContext.SaveChangesAsync();

            return ResponseHelper.CreateResponse(true, StatusCodes.Status201Created, "Log Created Successfully");
        }
  }
}