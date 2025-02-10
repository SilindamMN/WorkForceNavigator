namespace API.Controllers
{
  using Application.Interfaces;
  using Application.Services;
  using Domain.Dtos.LeaveRequest;
  using Domain.Dtos.Timesheet;
  using Domain.Entities.TimeSheets;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using System;

  [Route("api/[controller]")]
  [ApiController]
  public class TimesheetController : ControllerBase
  {
    private readonly ITimesheetService timesheetService;

    public TimesheetController(ITimesheetService timesheetService)
    {
      this.timesheetService = timesheetService;
    }

    [HttpPost]
    [Authorize]
    [Route("Create")]
    public async Task<IActionResult> CreateTimesheetEntry([FromBody] TimesheetCreateModifyDto timesheetEntry)
    {
      var result = await timesheetService.TimesheetEntry(User, timesheetEntry);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }

    [HttpGet]
    [Route("DAte")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GroupedTimesheetDetailDto>>> GetTimeSheetByDate(DateTime date)
    {
      DateTime dates = date.Date;
      var timesheets = await timesheetService.GetTimesheetEntries(User, dates);

      if (timesheets == null)
      {
        return NotFound(); // Return HTTP 404 Not Found if user not found
      }
      return Ok(timesheets);
    }

    [HttpGet]
    [Route("Weekly")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GroupedTimesheetDetailDto>>> GetWeeklyTimesheetEntries()
    {
      var timesheets = await timesheetService.GetWeeklyTimesheetEntries(User);

      if (timesheets == null)
      {
        return NotFound(); // Return HTTP 404 Not Found if user not found
      }
      return Ok(timesheets);
    }
    [Route("DailyHour")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<DailyProjectTotalDto>>> GetDailyProjectHours(DateTime date)
    {
      var timesheets = await timesheetService.GetDailyProjectHours(User, date);

      if (timesheets == null)
      {
        return NotFound(); // Return HTTP 404 Not Found if user not found
      }
      return Ok(timesheets);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<DailyProjectTotalDto>>> GetWeeklyProjectHours(int weekOffSet)
    {
      var timesheets = await timesheetService.GetWeeklyProjectHours(User, weekOffSet);

      if (timesheets == null || !timesheets.Any())
      {
        return NoContent(); // Return HTTP 404 Not Found if user not found
      }
      return Ok(timesheets);
    }
  }
}