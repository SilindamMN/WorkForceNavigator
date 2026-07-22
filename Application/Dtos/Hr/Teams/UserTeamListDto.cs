namespace Application.Dtos.Hr.Teams
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class UserTeamListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public string TeamLeader { get; set; }  = string.Empty;
    }
}