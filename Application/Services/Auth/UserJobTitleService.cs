namespace Application.Services.Auth
{
    using Application.Dtos.Hr.JobTitles;
    using Application.Helpers;
    using Application.Interfaces.Auth;
    using Domain.Account;
    using Domain.Constants.Enums;
    using Domain.Dtos.General;
    using Domain.Enties;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UserJobTitleService : IUserJobTitleService
  {
    private readonly DataContext dataContext;

    public UserJobTitleService(DataContext dataContext)
    {
      this.dataContext = dataContext;
    }

    public async Task<GeneralServiceResponseDto> AssignJobTitleToUser(AssignJobTitleDto assignJobTitle)
    {
      var user = await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == assignJobTitle.username);
      //assign the jobTitle id
      user.JobTitleId = assignJobTitle.jobTitleId;
      await dataContext.SaveChangesAsync();
      return ResponseHelper.CreateResponse(true, 200, "JobTitle Assigned Successfully");
    }

    public Task<IEnumerable<ApplicationUser>> GetUsersByJobTitle(string title)
    {
      throw new NotImplementedException();
    }

    public async Task<JobTitleDto?> GetJobTitleForUser(string username)
    {
      var user = await dataContext.Users.SingleAsync(u => u.UserName == username);

     return user.JobTitleId.HasValue ? await GetJobTitleInfo(user.JobTitleId.Value) : null;

    }

    //will get the jobTitle including department name
    private async Task<JobTitleDto?> GetJobTitleInfo(int jobTitleId)
    {
      return await (from jobTitle in dataContext.JobTitles
                    join department in dataContext.Departments on jobTitle.DepartmentId equals department.Id
                    where jobTitle.Id == jobTitleId
                    select new JobTitleDto
                    {
                      Title = jobTitle.Title,
                      DepartmentName = department.DepartmentName,
                      Seniority = jobTitle.Seniority.ToString() // Assuming Seniority is an enum
                    }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<JobTitleDto>> GetJobTitles()
    {

      var jobTitlesWithDepartments = await dataContext.JobTitles
      .Include(jt => jt.Department)
      .Select(jt => new JobTitleDto
      {
        Title = jt.Title,
        DepartmentName = jt.Department.DepartmentName,
        Description = jt.Description,
        Seniority = jt.Seniority.ToString()
      })
      .ToListAsync();
      return jobTitlesWithDepartments;
    }


        public Task<GeneralServiceResponseDto> AssignSeniorityToUser(int jobtitleId)
        {
            throw new Exception("JobTitleId is null for the user.");
        }

        public async Task<List<JobTitleDto>> GetJobTitleByDepartmentAndSeniorityAsync(int departmentId, Seniority? seniority)
        {
            return await dataContext.JobTitles
                .AsNoTracking()
                .Where(jt => jt.DepartmentId == departmentId)
                .Where(jt => !seniority.HasValue || jt.Seniority == seniority.Value)
                .Select(jt => new JobTitleDto
                {
                    JobTitleId = jt.Id,
                    Title = jt.Title,
                    Seniority = jt.Seniority.ToString()
                }).ToListAsync();
        }
    }
}