namespace Domain.Enties.hr
{
    using Domain.Enties.Hr;
    using Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Department : BaseEntity<int>
  {
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
    public ICollection<JobTitle>? JobTitles { get; set; }
        public ICollection<Client>? Clients { get; set; }
    }
}