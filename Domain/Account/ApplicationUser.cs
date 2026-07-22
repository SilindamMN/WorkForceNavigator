namespace Domain.Account
{
    using Domain.Constants.Enums;
    using Domain.Enties.hr;
    using Domain.Enties.Hr;
    using Domain.Enties.Leaves;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ApplicationUser : IdentityUser
  {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        [NotMapped]
        public IList<string>? Roles { get; set; }

        public int? JobTitleId { get; set; }
        public JobTitle? JobTitle { get; set; }

        public Gender? Gender { get; set; }
        public decimal? Salary { get; set; }
        public Seniority? Seniority { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveAllocation> LeaveAllocations { get; set; } = new List<LeaveAllocation>();
    }
}