namespace Domain.Dtos.Timesheet
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;
  using System.Text;
  using System.Text.Json.Serialization;
  using System.Threading.Tasks;

  public class TimesheetDto
  {
    [Column(TypeName = "Date")]
    public DateTime TimesheetDate { get; set; }
    public string Description { get; set; }
    public int TimeSpent { get; set; }
    public int ProjectId { get; set; }
  }
}