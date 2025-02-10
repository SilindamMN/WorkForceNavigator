namespace Domain.Dtos.Account
{
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class UpdateUserDetailsDto
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public string? JobTitle { get; set; }
    public decimal? Salary { get; set; }
    public string  Phonenumber { get; set; }
  }
}