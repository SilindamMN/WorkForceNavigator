namespace Domain.Dtos.LeaveAllocation
{
  using Domain.Enties.Leaves;
  using Domain.Enties;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Domain.Enums;
  using Domain.Account;

  public class LeaveAllocationDto
  {
    public int NumberOfDays { get; set; }
    public string Username { get; set; }
    public string LeaveName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}