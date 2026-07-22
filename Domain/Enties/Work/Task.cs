namespace Domain.Enties.Work
{
  using Domain.Entities;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Task : BaseEntity<int>
  {
    public int ProjectID { get; set; }
    public string TaskName { get; set; } = string.Empty;
  }
}