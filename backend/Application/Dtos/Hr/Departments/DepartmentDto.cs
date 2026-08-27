namespace Application.Dtos.Hr.Departments
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class DepartmentDto
  {
    public int Id { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
  }
}