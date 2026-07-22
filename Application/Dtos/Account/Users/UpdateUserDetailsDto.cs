namespace Application.Dtos.Account.Users
{
    using Domain.Constants.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UpdateUserDetailsDto
  {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    public Gender? Gender { get; set; }
        public int JobTitleId { get; set; }
        public int? TeamId { get; set; }
        public Seniority? Seniority { get; set; }
        public decimal? Salary { get; set; }
        public string Phonenumber { get; set; } = string.Empty;
  }
}