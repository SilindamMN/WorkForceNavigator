namespace Application.Interfaces
{
  using Domain.Dtos.JobTitles;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  public interface IJobTitleService
    {
    Task<IEnumerable<JobTitleDto>> GetAllJobTitlesAsync();
    Task<JobTitleDto> GetJobTitleByIdAsync(int id);
    Task<int> CreateJobTitleAsync(UpdateCreateJobTitleDto jobTitleDto);
    Task UpdateJobTitleAsync(int id, UpdateCreateJobTitleDto jobTitleDto);
    Task DeleteJobTitleAsync(int id);
    Task  AssignJobTitle(AssignJobTitleDto jobTitleDto);
  }
}