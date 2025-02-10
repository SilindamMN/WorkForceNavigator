namespace Domain.Dtos.JobTitles
{
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public class UpdateCreateJobTitleDto
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public Seniority Seniority { get; set; }
    public int DepartmentId { get; set; }
  }
}