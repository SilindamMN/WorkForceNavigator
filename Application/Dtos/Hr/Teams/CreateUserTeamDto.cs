namespace Application.Dtos.Hr.Teams
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class CreateUserTeamDto
    {
        public string UserId { get; set; } = string.Empty;
        public int TeamId { get; set; }
        public bool IsRemove { get; set; }

    }
}