namespace Application.Interfaces
{
  using Domain.Account;
  using Domain.Dtos.General;
  using Domain.Dtos.JobTitles;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public interface IUserJobTitleService
  {
    Task<GeneralServiceResponseDto> AssignJobTitleToUser(AssignJobTitleDto assignJobTitle);
    Task<JobTitleDto> GetJobTitleForUser(string username);
    Task<IEnumerable<ApplicationUser>> GetUsersByJobTitle(string title);
    Task<IEnumerable<JobTitleDto>> GetJobTitles();
  }
}