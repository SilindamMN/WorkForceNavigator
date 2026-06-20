namespace Domain.Enties.Leaves
{
  using Domain.Account;
  using Domain.Entities;
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

  public class LeaveAllocation : BaseEntity<int>
  {
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }
        public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public string Username { get; set; } 
  }
}