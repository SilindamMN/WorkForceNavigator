namespace Domain.Enties
{
  using Domain.Entities;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Department : BaseEntity<int>
  {
    public string DepartmentName { get; set; }
    public string Description { get; set; }
    // Navigation property to access the associated job titles
    public ICollection<JobTitle> JobTitles { get; set; }
  }
}