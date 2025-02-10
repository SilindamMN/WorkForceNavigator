namespace Domain.Enties.TimeSheets
{
  using Domain.Account;
  using Domain.Entities;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Team : BaseEntity<int>
  {
    public string TeamName { get; set; }
    public string Description { get; set; }
    public string TeamLeader { get; set; }
    public IEnumerable<Project> Project { get; set; }
    // Navigation property for many-to-many relationship with ApplicationUser
    public ICollection<UserTeam> UserTeams { get; set; }
  }
}