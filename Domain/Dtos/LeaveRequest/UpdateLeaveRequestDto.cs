namespace Domain.Dtos.LeaveRequest
{
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Text.Json.Serialization;
  using System.Threading.Tasks;

  public class UpdateLeaveRequestDto
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? Comment { get; set; }
  }
}