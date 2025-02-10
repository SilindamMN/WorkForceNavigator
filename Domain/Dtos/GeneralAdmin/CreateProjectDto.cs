namespace Domain.Dtos.GeneralAdmin
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class CreateProjectDto
  {
    public string ProjectName { get; set; }
    public int ClientId { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TeamId { get; set; }
  }
}