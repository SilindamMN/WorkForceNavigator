namespace Application.Dtos.Hr.Teams
{
    using System.Collections.Generic;

    public class TeamMemberDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
    }

    public class TeamMemberDetailsDto
    {
        public List<TeamMemberDto> Members { get; set; } = new();
        public List<string> ProjectList { get; set; } = new();
    }
}