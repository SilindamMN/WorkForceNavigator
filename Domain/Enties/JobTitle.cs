namespace Domain.Enties
{
  using Domain.Account;
  using Domain.Entities;
  using Domain.Enums;
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