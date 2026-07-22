namespace Application.Dtos.Work.Timesheet
{
    using System;

    public class TimesheetDetailDto
    {
        public int Id { get; set; }

        public DateTime TimesheetDate { get; set; }

        public string DayName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int TimeSpent { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;
    }
}