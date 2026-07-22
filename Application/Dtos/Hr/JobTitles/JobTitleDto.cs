namespace Application.Dtos.Hr.JobTitles
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public class JobTitleDto
  {
        public int? JobTitleId { get; set; }
        public string? Title { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Seniority { get; set; } = string.Empty;
  }
}