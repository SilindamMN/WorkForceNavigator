namespace Application.Services.Auth
{
    using Application.Dtos.Account.Logs;
    using Application.Interfaces.Auth;
    using AutoMapper;
    using Domain.Enties.Hr;
    using Domain.Entities;
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

    public async Task<IEnumerable<GetLogDto>> getMyLogsAsync(ClaimsPrincipal User)
    {
      var logs = await dataContext.Logs.Where(u=>u.Username==User.Identity.Name).OrderByDescending(x => x.CreatedAt).ToListAsync();
      return mapper.Map<IEnumerable<GetLogDto>>(logs);
    }

    public async Task SaveNewLogAsync(string UserName, string Description)
    {
      var newLog = new Log()
      {
        Username = UserName,
        Description = Description
      };
      await dataContext.Logs.AddAsync(newLog);
      await dataContext.SaveChangesAsync();
    }
  }
}