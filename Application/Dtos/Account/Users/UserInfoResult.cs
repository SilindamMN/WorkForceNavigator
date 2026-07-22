namespace Application.Dtos.Account.Users
{
    using Domain.Constants.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class UserInfoResult
  {
    public string Id { get; set; } = string.Empty.ToString();
    public string FirstName { get; set; } = string.Empty.ToString();
    public string LastName { get; set; } = string.Empty.ToString();
    public string Email { get; set; } = string.Empty.ToString();
    public string Username { get; set; } = string.Empty.ToString();
    public DateTime CreatedAt { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty.ToString();
        public int? JobTitleId { get; set; }
        public string JobTitleName { get; set; } = string.Empty.ToString();
        public int? TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty.ToString();
        public string PhoneNumber { get; set; } = string.Empty.ToString();
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Seniority? Seniority { get; set; }
        public Gender? Gender { get; set; }
    public IEnumerable<string>? Roles { get; set; }
  }
}