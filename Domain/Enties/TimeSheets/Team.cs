namespace Domain.Enties.TimeSheets
{
  using Domain.Account;
  using Domain.Entities;
  using System;
  using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

  public class Team : BaseEntity<int>
  {
    public string TeamName { get; set; }
    public string Description { get; set; }
        [ForeignKey("TeamLeaderId")]

        public string TeamLeader { get; set; }
        public ICollection<Project> Projects { get; set; }   
        // Navigation property for many-to-many relationship with ApplicationUser
        public ICollection<UserTeam> UserTeams { get; set; }
  }
}