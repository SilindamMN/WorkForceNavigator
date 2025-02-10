namespace Application.Interfaces
{
  using Domain.Dtos.General;
  using Domain.Dtos.LeaveAllocation;
  using Domain.Dtos.Timesheet;
  using Domain.Entities.TimeSheets;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public interface ITimesheetService
  {
    Task<GeneralServiceResponseDto> TimesheetEntry(ClaimsPrincipal User, TimesheetCreateModifyDto TimesheetEntry);
    Task<int> GetTotalTimeSpentByDate(ClaimsPrincipal User, DateTime date);
    Task<IEnumerable<GroupedTimesheetDetailDto>> GetTimesheetEntries(ClaimsPrincipal User,DateTime date);
    Task<IEnumerable<GroupedTimesheetDetailDto>> GetWeeklyTimesheetEntries(ClaimsPrincipal User);
    Task<DailyProjectTotalDto> GetDailyProjectHours(ClaimsPrincipal user, DateTime date);
    Task<IEnumerable<DailyProjectTotalDto>> GetWeeklyProjectHours(ClaimsPrincipal user,int weekOffSet);
  }
}