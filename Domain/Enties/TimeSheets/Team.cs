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
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        // FK
        public string TeamLeaderId { get; set; }

        // Navigation
        public ApplicationUser TeamLeader { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
    }
}