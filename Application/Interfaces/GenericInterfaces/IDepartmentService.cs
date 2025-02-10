namespace Application.Interfaces.GenericInterfaces
{
  using Domain.Dtos.GeneralAdmin;
  using Domain.Dtos.JobTitles;
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