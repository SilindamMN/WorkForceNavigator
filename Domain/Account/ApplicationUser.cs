namespace Domain.Account
{
  using Domain.Enties;
  using Domain.Enties.TimeSheets;
  using Domain.Enums;
  using Microsoft.AspNetCore.Identity;
  using System.ComponentModel.DataAnnotations.Schema;
  public class ApplicationUser : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [NotMapped]
    public IList<string> Roles { get; set; }

    public int? JobTitleId { get; set; }
    public JobTitle JobTitle { get; set; }
    public string? LineManager { get; set; }
    public Gender? Gender { get; set; }
    public decimal? Salary { get; set; }
    public Seniority? Seniority { get; set; }
    public string? PhoneNumber { get; set; }
    // Navigation property for many-to-many relationship with Team
    public ICollection<UserTeam> UserTeams { get; set; }
  }
}