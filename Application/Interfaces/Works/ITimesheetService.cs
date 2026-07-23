namespace Application.Interfaces.Works
{
    using Application.Dtos.Work.Timesheet;
    using Domain.Dtos.General;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITimesheetService
  {
    Task<GeneralServiceResponseDto> TimesheetEntryAsync(ClaimsPrincipal user, TimesheetCreateModifyDto timesheetEntry);
    Task<int> GetTotalTimeSpentByDateAsync(ClaimsPrincipal User, DateTime date);
    Task<IEnumerable<TimesheetDetailDto>> GetTimesheetEntriesAsync(ClaimsPrincipal user,DateTime date);
    Task<IEnumerable<TimesheetDetailDto>> GetWeeklyTimesheetEntriesAsync(ClaimsPrincipal User);
    Task<DailyProjectTotalDto> GetDailyProjectHoursAsync(ClaimsPrincipal user, DateTime date);
    Task<IEnumerable<DailyProjectTotalDto>> GetWeeklyProjectHoursAsync(ClaimsPrincipal user,int weekOffSet);
  }
}