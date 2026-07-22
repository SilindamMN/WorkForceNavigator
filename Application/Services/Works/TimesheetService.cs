namespace Application.Services.Works
{
    using Application.Helpers;
    using AutoMapper;
    using Domain.Account;
    using Domain.Dtos.General;
    using Microsoft.AspNetCore.Identity;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;
    using Domain.Enties.Work;
    using Application.Dtos.Work.Timesheet;
    using Application.Dtos.Work.Projects;
    using Application.Interfaces.Works;
    using Application.Interfaces;

    public class TimesheetService : ITimesheetService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private readonly IGenericService<Project, ProjectDto> genericService;

        public TimesheetService(DataContext dataContext, IMapper mapper, IGenericService<Project, ProjectDto> genericService)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.genericService = genericService;
        }

        public async Task<IEnumerable<TimesheetDetailDto>> GetTimesheetEntriesAsync(
           ClaimsPrincipal user,
           DateTime date)
        {
            var entries = await (
                from ts in dataContext.TimesheetEntries
                join p in dataContext.Projects
                    on ts.ProjectId equals p.Id
                where ts.TimesheetDate.Date == date.Date
                      && ts.Username == user.Identity.Name
                orderby ts.TimesheetDate, ts.Id
                select new TimesheetDetailDto
                {
                    Id = ts.Id,
                    TimesheetDate = ts.TimesheetDate,
                    DayName = ts.TimesheetDate.DayOfWeek.ToString(),
                    Username = ts.Username,
                    Description = ts.Description,
                    TimeSpent = ts.TimeSpent,
                    ProjectId = ts.ProjectId,
                    ProjectName = p.ProjectName
                })
                .ToListAsync();

            return entries;
        }

        public async Task<int> GetTotalTimeSpentByDateAsync(ClaimsPrincipal user, DateTime date)
        {
            var username = user?.Identity?.Name;
            var timeSpent = dataContext.TimesheetEntries
                                   .Where(t => t.TimesheetDate.Date == date.Date && t.Username == username)
                                   .Sum(t => t.TimeSpent);
            return timeSpent;
        }

        public async Task<IEnumerable<TimesheetDetailDto>> GetWeeklyTimesheetEntriesAsync(ClaimsPrincipal User)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Calculate the start and end dates of the week (Monday to Friday)
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(4);

            // Initialize a list to hold all timesheet entries for the week
            List<TimesheetDetailDto> weeklyTimesheetEntries = new List<TimesheetDetailDto>();

            // Iterate over each day of the week from Monday to Friday
            for (DateTime day = startOfWeek; day <= endOfWeek; day = day.AddDays(1))
            {
                // Fetch timesheet entries for the current day
                var dailyEntries = await GetTimesheetEntriesAsync(User, day);

                // Directly add the daily entries to the weekly list
                weeklyTimesheetEntries.AddRange(dailyEntries);
            }

            return weeklyTimesheetEntries;
        }

        public async Task<GeneralServiceResponseDto> TimesheetEntryAsync(ClaimsPrincipal user, TimesheetCreateModifyDto timesheetEntryDto)
        {
                var username = user?.Identity?.Name;

                // Retrieve the project and ensure it exists
                var project = await genericService.GetByIdAsync(timesheetEntryDto.ProjectId);
                if (project == null)
                {
                    return ResponseHelper.CreateResponse(false, 404, "Project not found.");
                }
                //user can insert many entry for a specific day as much as they are less than 8 hours
                var hourSpent = await GetTotalTimeSpentByDateAsync(user, timesheetEntryDto.TimesheetDate);
                var updateHours = hourSpent + timesheetEntryDto.TimeSpent;

                if (hourSpent < 8 && updateHours <= 8)
                {
                    var timesheetEntry = new TimesheetCreateModifyDto
                    {
                        TimesheetDate = timesheetEntryDto.TimesheetDate,
                        Description = timesheetEntryDto.Description,
                        TimeSpent = timesheetEntryDto.TimeSpent,
                        ProjectId = timesheetEntryDto.ProjectId
                    };

                    var map = mapper.Map<TimesheetEntry>(timesheetEntry);
                    map.Username = username ?? string.Empty;
                    dataContext.TimesheetEntries.Add(map);
                    await dataContext.SaveChangesAsync();

                    // Return a success response
                    return ResponseHelper.CreateResponse(true, 200, "Timesheet entry successfully created.");
                }
                return ResponseHelper.CreateResponse(false, 501, "You can only work 8 hours per day " + "You have already worked " + hourSpent + "Hours");
            
        }

        public async Task<DailyProjectTotalDto> GetDailyProjectHoursAsync(ClaimsPrincipal user, DateTime date)
        {
            var username = user?.Identity?.Name;

            var timesheetEntries = dataContext.TimesheetEntries
                .Where(t => t.TimesheetDate.Date == date.Date && t.Username == username)
                .Select(t => new
                {
                    t.TimeSpent,
                    t.ProjectId
                })
                .ToList();

            var projectIds = timesheetEntries.Select(t => t.ProjectId).Distinct();
            var projects = dataContext.Projects
                .Where(p => projectIds.Contains(p.Id))
                .Select(p => new { p.Id, p.ProjectName })
                .ToList();

            var projectIdToNameMap = projects.ToDictionary(p => p.Id, p => p.ProjectName);

            var totalHours = timesheetEntries.Sum(t => t.TimeSpent);
            var projectNames = new HashSet<string>(timesheetEntries.Select(t => projectIdToNameMap[t.ProjectId]));

            return new DailyProjectTotalDto
            {
                Date = date,
                TotalHours = totalHours,
                ProjectNames = projectNames,
                DayName = date.DayOfWeek.ToString()
            };
        }

        public async Task<IEnumerable<DailyProjectTotalDto>> GetWeeklyProjectHoursAsync(ClaimsPrincipal user, int weekOffSet)
        {
            DateTime today = DateTime.Today;
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday).AddDays(7 * weekOffSet);
            DateTime endOfWeek = startOfWeek.AddDays(4);

            List<DailyProjectTotalDto> weeklyProjectTotalHoursList = new List<DailyProjectTotalDto>();

            for (DateTime day = startOfWeek; day <= endOfWeek; day = day.AddDays(1))
            {
                var dailyProjectHours = await GetDailyProjectHoursAsync(user, day);
                weeklyProjectTotalHoursList.Add(dailyProjectHours);
            }
            return weeklyProjectTotalHoursList;
        }
    }
}