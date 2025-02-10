namespace Domain.Dtos.Timesheet
{
  using Domain.Enties;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class TimesheetDailyDto
  {
    public int TotalHours { get; set; }

    public HashSet<string> ProjectNames { get; set; }

    [Column(TypeName = "Date")]
    public DateTime TimesheetDate { get; set; }
    public string  Day { get; set; }
  }
}