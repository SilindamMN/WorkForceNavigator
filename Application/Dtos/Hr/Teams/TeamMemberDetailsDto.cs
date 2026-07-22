namespace Application.Dtos.Hr.Teams
{
    using Domain.Enties.Work;
    using System.Collections.Generic;

    public class TeamMemberDetailsDto
  {
    public string TeamName { get; set; } = string.Empty;
        public string TeamLeader { get; set; } = string.Empty;
    public List<Project> Projects { get; set; } = new List<Project>();

    public List<MemberDetails>? MemberDetails { get; set; }
  }
  public class MemberDetails
  {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
  }
}