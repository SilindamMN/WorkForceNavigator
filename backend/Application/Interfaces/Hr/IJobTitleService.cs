namespace Application.Interfaces.Hr
{
    using Application.Dtos.Hr.JobTitles;
    using Domain.Dtos.General;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IJobTitleService
    {
        Task<IEnumerable<JobTitleDto>> GetAllJobTitlesAsync();
        Task<JobTitleDto> GetJobTitleByIdAsync(int id);
        Task<IEnumerable<JobTitleDto?>> GetJobTitleByDepartmentAsync(int departmentId);
        Task<GeneralServiceResponseDto> CreateJobTitleAsync(UpdateCreateJobTitleDto jobTitleDto);
        Task<GeneralServiceResponseDto> UpdateJobTitleAsync(int id, UpdateCreateJobTitleDto jobTitleDto);
        Task<GeneralServiceResponseDto> DeleteJobTitleAsync(int id);
        Task<GeneralServiceResponseDto> AssignJobTitle(AssignJobTitleDto jobTitleDto);
    }
}