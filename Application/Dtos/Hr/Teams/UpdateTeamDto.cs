namespace Application.Dtos.Hr.Teams
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class UpdateTeamDto
  {
    public string TeamName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
  }
}