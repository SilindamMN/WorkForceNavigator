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
        Task<GeneralServiceResponseDto> AssignJobTitleToUser(AssignJobTitleDto assignJobTitle);
        Task<GeneralServiceResponseDto> AssignSeniorityToUser(int jobtitleId);
        Task<JobTitleDto?> GetJobTitleForUser(string username);
    Task<IEnumerable<ApplicationUser>> GetUsersByJobTitle(string title);
    Task<IEnumerable<JobTitleDto>> GetJobTitles();
        Task<List<JobTitleDto>> GetJobTitleByDepartmentAndSeniorityAsync(int departmentId, Seniority? seniority);
    }
}