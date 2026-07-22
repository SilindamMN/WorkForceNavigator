namespace Application.Dtos.Leaves.LeaveAllocation
{
  using Domain.Enties.Leaves;
  using Domain.Enties;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Domain.Account;

  public class LeaveAllocationDto
  {
    public int NumberOfDays { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LeaveName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
  }
}