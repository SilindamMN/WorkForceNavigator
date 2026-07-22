namespace Domain.Enties.Hr
{
    using Domain.Enties.hr;
    using Domain.Enties.Work;
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
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}