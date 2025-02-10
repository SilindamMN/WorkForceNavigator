namespace Domain.Dtos.LeaveAllocation
{
  using Domain.Enties.Leaves;
  using Domain.Enties;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class EmployeeLeaveAllocationDto
  {
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public string LeaveName { get; set; }
  }
}