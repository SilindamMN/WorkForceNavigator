namespace Domain.Enties.TimeSheets
{
  using Domain.Entities;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Client : BaseEntity<int>
  {
    public string ClientName { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string Email { get; set; }
    public IEnumerable<Project> Projects { get; set; }
  }
}