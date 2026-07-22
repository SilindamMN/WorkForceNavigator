namespace Application.Interfaces.Hr
{
    using Application.Dtos.Hr.JobTitles;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IJobTitleService
    {
    Task<IEnumerable<JobTitleDto>> GetAllJobTitlesAsync();
        Task<JobTitleDto> GetJobTitleByIdAsync(int id);
        Task<List<JobTitleDto>> GetJobTitleByDepartmentAsync(int departmentId);
        Task<int> CreateJobTitleAsync(UpdateCreateJobTitleDto jobTitleDto);
    Task UpdateJobTitleAsync(int id, UpdateCreateJobTitleDto jobTitleDto);
    Task DeleteJobTitleAsync(int id);
    Task  AssignJobTitle(AssignJobTitleDto jobTitleDto);
  }
}