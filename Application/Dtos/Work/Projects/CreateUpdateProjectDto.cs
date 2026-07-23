namespace Application.Dtos.Work.Projects
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class CreateUpdateProjectDto
  {
    public string ProjectName { get; set; } = string.Empty;
    public int ClientId { get; set; }
       public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TeamId { get; set; }
  }
}