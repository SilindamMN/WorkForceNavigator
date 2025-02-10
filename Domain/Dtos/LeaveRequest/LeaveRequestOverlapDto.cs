namespace Domain.Dtos.LeaveRequest
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class LeaveRequestOverlapDto
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}