namespace Application.Dtos.Leaves.LeaveRequest
{
    using Domain.Constants.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LeaveRequestDto
  {
    public int Id { get; set; }
    public string LeaveName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RequestedDate { get; set; }
    public Status Status { get; set; }
    public string FirstName { get; set; } = string.Empty ;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    public int NumberOfDays { get; set; }
  }
}