namespace Domain.Dtos.LeaveRequest
{
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class LeaveRequestDto
  {
    public int Id { get; set; }
    public string LeaveName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RequestedDate { get; set; }
    public Status Status { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public int NumberOfDays { get; set; }
  }
}