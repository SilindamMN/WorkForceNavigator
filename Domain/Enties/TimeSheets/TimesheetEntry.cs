namespace Domain.Entities.TimeSheets
{
    using Domain.Account;
    using Domain.Enties;
    using System;
  using System.ComponentModel.DataAnnotations.Schema;
  public class TimesheetEntry : BaseEntity<int>
  {
    public DateTime TimesheetDate { get;  set; }
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }
        public string Description { get; set; }
    public int TimeSpent { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } // Navigation property to Project
  }
}