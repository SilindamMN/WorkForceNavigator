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
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? JobTitleId { get; set; }
        public string JobTitleName { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        public string PhoneNumber { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Seniority? Seniority { get; set; }
        public Gender? Gender { get; set; }
    public IEnumerable<string> Roles { get; set; }
  }
}