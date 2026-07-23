namespace Application.Interfaces.Auth
{
    using Application.Dtos.Account.Logs;
    using Domain.Dtos.General;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILogService
  {
    Task <GeneralServiceResponseDto> SaveNewLogAsync(string userName, string description);
    Task<IEnumerable<GetLogDto>> getLogsAsync();
    Task<IEnumerable<GetLogDto>> getMyLogsAsync(ClaimsPrincipal user);
  }
}