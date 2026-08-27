namespace Application.Dtos.Work.Projects
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class ProjectDto
  {
    public string ProjectName { get; set; } = string.Empty; 
    public string ClientName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}