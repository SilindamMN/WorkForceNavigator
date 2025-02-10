namespace Domain.Entities.TimeSheets
{
  using System;
  using System.ComponentModel.DataAnnotations.Schema;
  public class TimesheetCreateModifyDto 
  {
    [Column(TypeName = "Date")]
    public DateTime TimesheetDate { get;  set; }
    public string Description { get; set; }
    public int TimeSpent { get;  set; }
    public int ProjectId { get; set; }

    public TimesheetCreateModifyDto()
    {
    }

    public TimesheetCreateModifyDto(DateTime timesheetDate, string description, int timeSpent, int projectId)
    {
      TimesheetDate = ValidateTimesheetDate(timesheetDate);
      Description = description;
      TimeSpent = CheckTimeSpent(timeSpent);
      ProjectId = projectId;
    }

    private DateTime ValidateTimesheetDate(DateTime value)
    {
      return (value.DayOfWeek >= DayOfWeek.Monday && value.DayOfWeek <= DayOfWeek.Friday) ?
                  value.Date :
                  (value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday) ?
                      throw new ArgumentException("TimesheetDate must be a weekday (Monday to Friday).") :
                      throw new ArgumentException("Invalid TimesheetDate provided.");
    }

    private int CheckTimeSpent(int value)
    {
      return (value == 0) ? throw new ArgumentException("TimeSpent must not be less than 0.")
         : (value > 8) ? throw new ArgumentException("TimeSpent must not be greater than 8.")
         : value;
    }
  }
}