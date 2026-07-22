namespace Application.Dtos.Account.Users
{
    using Domain.Constants.Enums;
    using System;
    using System.Collections.Generic;

    public class UserDetailsDto
  {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    public IEnumerable<string>? Roles { get; set; }
    public Gender? Gender { get; set; }
        public int? JobTitleId { get; set; }
        public string? JobTitle { get; set; }
    public decimal? Salary { get; set; }

        public int? DepartmentId { get; set; }
        public string Department { get; set; } = string.Empty.ToString();
        public int? TeamId { get; set; }
        public string TeamName { get; set; } =string.Empty.ToString();
        public Seniority Seniority { get; set; }
    public DateTime JoiningDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
  }
}