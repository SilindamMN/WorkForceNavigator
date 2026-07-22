namespace API.Controllers.Auth
{
    using Application.Dtos.Account.Logs;
    using Application.Interfaces.Auth;
    using Domain.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/logs")]
  [ApiController]
  public class LogsController : ControllerBase
  {
    private readonly ILogService logService;

    public LogsController(ILogService logService)
    {
      this.logService = logService;
    }

    [HttpGet]
    [Authorize(Roles = StaticUserRoles.OwnerAdmin)]
    public async Task<ActionResult<IEnumerable<GetLogDto>>> GetLogs()
    {
      var logs = await logService.getLogsAsync();
      return Ok(logs);
    }

    [HttpGet]
    [Route("mine")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetLogDto>>> GetMyLogs()
    {
      var logs = await logService.getMyLogsAsync(User);
      return Ok(logs);
    }
  }
}
