namespace Application.Interfaces.Hr
{
    using Application.Dtos.Hr.JobTitles;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDepartmentService
  {
    Task<IEnumerable<UserDetailJobTitle>> GetUserJobTitleTeamsListAsync(int id);
  }
}