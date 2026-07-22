namespace Application.Interfaces.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Application.Dtos.Hr.JobTitles;
    using Domain.Account;
    using Domain.Constants.Enums;
    using Domain.Dtos.General;

    public interface IUserJobTitleService
    {
        Task<GeneralServiceResponseDto> AssignJobTitleToUserAsync(AssignJobTitleDto assignJobTitle);
        Task<GeneralServiceResponseDto> AssignSeniorityToUserAsync(int jobtitleId);
        Task<JobTitleDto?> GetJobTitleForUserAsync(string username);
        Task<IEnumerable<ApplicationUser>> GetUsersByJobTitleAsync(string title);
        Task<IEnumerable<JobTitleDto>> GetJobTitlesAsync();
        Task<IEnumerable<JobTitleDto>> GetJobTitleByDepartmentAndSeniorityAsync(int departmentId, Seniority? seniority);
    }
}