namespace Domain.Account
{
  using Domain.Enties;
    using Domain.Enties.Leaves;
    using Domain.Enties.TimeSheets;
    using Domain.Enums;
  using Microsoft.AspNetCore.Identity;
  using System.ComponentModel.DataAnnotations.Schema;
  public class ApplicationUser : IdentityUser
  {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // FIX: use UtcNow

        [NotMapped]
        public IList<string> Roles { get; set; }

        public int? JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }

        // FIX: was string "LineManager" (email) — now a proper self-referencing FK
        public string? LineManagerId { get; set; }
        [ForeignKey("LineManagerId")]
        public ApplicationUser LineManager { get; set; }

        public Gender? Gender { get; set; }
        public decimal? Salary { get; set; }
        public Seniority? Seniority { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveAllocation> LeaveAllocations { get; set; } = new List<LeaveAllocation>();
    }
}