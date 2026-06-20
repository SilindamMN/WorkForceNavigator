namespace Domain.Dtos.Account
{
  using Domain.Enums;
  using System;
  using System.Collections.Generic;

  public class UserDetailsDto
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public Gender? Gender { get; set; }
    public string? JobTitle { get; set; }
    public decimal? Salary { get; set; }
    public string? LineManager { get; set; }
    public string Department { get; set; }
    public Seniority Seniority { get; set; }
    public DateTime JoiningDate { get; set; }
    public string PhoneNumber { get; set; }
  }
}