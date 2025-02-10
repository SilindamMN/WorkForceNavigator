namespace Application.Services.GenericServices
{
  using Application.Interfaces.GenericInterfaces;
  using Domain.Dtos.GeneralAdmin;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class DepartmentService : IDepartmentService
  {
    private readonly DataContext dataContext;

    public DepartmentService(DataContext dataContext)
    {
      this.dataContext = dataContext;
    }

    public async Task<IEnumerable<UserDetailJobTitle>> GetUserJobTitleTeamsListAsync(int departmentId)
    {
      var details = await (from u in dataContext.Users
                           join jt in dataContext.UserTeams on u.Id equals jt.UserId
                           join j in dataContext.JobTitles on u.JobTitleId equals j.Id
                           join d in dataContext.Departments on j.DepartmentId equals d.Id
                           join tt in dataContext.Teams on jt.TeamId equals tt.Id
                           where d.Id == departmentId
                           select new UserDetailJobTitle
                           {
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Email = u.Email,
                             JobTitle = j.Title,
                             Team = tt.TeamName,
                           }).ToListAsync();

      return details;
    }
  }
}