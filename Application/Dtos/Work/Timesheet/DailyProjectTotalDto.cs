namespace Application.Dtos.Work.Timesheet
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public class DailyProjectTotalDto
  {
    [Column(TypeName = "Date")]
    public DateTime Date { get; set; }
    public int TotalHours { get; set; }
    public string DayName { get; set; } = string.Empty;
    public HashSet<string>? ProjectNames { get; set; }
  }
}