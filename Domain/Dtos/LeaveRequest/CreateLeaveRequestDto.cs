namespace Domain.Dtos.LeaveRequest
{
  using Domain.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Text.Json.Serialization;
  using System.Threading.Tasks;

  public class CreateLeaveRequestDto
  {
    public int LeaveTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [JsonIgnore]
    public int NumberOfDays { get; set; }
    // Add other properties as needed
  }
}