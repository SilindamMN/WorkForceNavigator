namespace Application.Interfaces.Auth
{
    using Application.Dtos.Account.Users;
    using Application.Dtos.Hr.JobTitles;
    using Domain.Account;
    using Domain.Constants.Enums;
    using Domain.Dtos.General;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserJobTitleService
    {
        Task<GeneralServiceResponseDto> AssignJobTitleToUserAsync(AssignJobTitleDto assignJobTitle);
        Task<JobTitleDto?> GetJobTitleForUserAsync(string username);
        Task<IEnumerable<JobTitleDto>> GetJobTitleByDepartmentAndSeniorityAsync(int departmentId, Seniority? seniority);
        Task<IEnumerable<JobTitleDto>> GetAllJobTitlesAsync();
    }
}