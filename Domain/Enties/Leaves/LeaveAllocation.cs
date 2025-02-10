namespace Domain.Enties.Leaves
{
  using Domain.Account;
  using Domain.Entities;
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class LeaveAllocation : BaseEntity<int>
  {
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public string Username { get; set; } 
  }
}