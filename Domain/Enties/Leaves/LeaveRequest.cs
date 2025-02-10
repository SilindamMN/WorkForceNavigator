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

  public class LeaveRequest : BaseEntity<int>
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }

    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public Status Status { get; set; }

    public int NumberOfDays { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string UserName { get; set; }
    // Constructor that accepts UserName
  }
}