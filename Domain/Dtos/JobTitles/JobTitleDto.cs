namespace Domain.Dtos.JobTitles
{
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public class JobTitleDto
  {
    public string Title { get; set; }
    public string DepartmentName { get; set; }
    public string Description { get; set; }
    public string Seniority { get; set; }
  }
}