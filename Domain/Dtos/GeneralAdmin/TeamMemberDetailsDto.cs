namespace Domain.Dtos.LeaveTypes.Teams
{
  using Domain.Enties;
  using Domain.Enties.TimeSheets;
  using System.Collections.Generic;

  public class TeamMemberDetailsDto
  {
    public string TeamName { get; set; }
    public string TeamLeader { get; set; }
    public List<Project> Projects { get; set; } = new List<Project>();

    public List<MemberDetails> MemberDetails { get; set; }
  }
  public class MemberDetails
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? JobTitle { get; set; }
  }
}