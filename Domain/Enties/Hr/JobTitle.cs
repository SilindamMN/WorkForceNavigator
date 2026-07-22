namespace Domain.Enties.hr
{
    using Domain.Account;
    using Domain.Constants.Enums;
    using Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class JobTitle : BaseEntity<int>
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public Seniority Seniority { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    // Navigation property to Employee
    public ICollection<ApplicationUser> Users { get; set; }
  }
}