namespace API.Controllers.Works
{
    using Application.Dtos.Work.Timesheet;
    using Application.Interfaces.Works;
    using Application.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/timesheet")]
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
        [Route("create")]
        public async Task<IActionResult> CreateTimesheetEntry([FromBody] TimesheetCreateModifyDto timesheetEntry)
        {
            var result = await timesheetService.TimesheetEntryAsync(User, timesheetEntry);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        [Route("by-date")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TimesheetDetailDto>>> GetTimeSheetByDate(DateTime date)
        {
            DateTime dates = date.Date;
            var timesheets = await timesheetService.GetTimesheetEntriesAsync(User, dates);

            if (timesheets == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if user not found
            }
            return Ok(timesheets);
        }

        [HttpGet]
        [Route("weekly")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TimesheetDetailDto>>> GetWeeklyTimesheetEntries()
        {
            var timesheets = await timesheetService.GetWeeklyTimesheetEntriesAsync(User);

            if (timesheets == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if user not found
            }
            return Ok(timesheets);
        }
        [Route("daily")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DailyProjectTotalDto>>> GetDailyProjectHours(DateTime date)
        {
            var timesheets = await timesheetService.GetDailyProjectHoursAsync(User, date);

            if (timesheets == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if user not found
            }
            return Ok(timesheets);
        }

        [HttpGet]
        [Route("week-off-set")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DailyProjectTotalDto>>> GetWeeklyProjectHours(int weekOffSet)
        {
            var timesheets = await timesheetService.GetWeeklyProjectHoursAsync(User, weekOffSet);

            if (timesheets == null || !timesheets.Any())
            {
                return NoContent(); // Return HTTP 404 Not Found if user not found
            }
            return Ok(timesheets);
        }
    }
}