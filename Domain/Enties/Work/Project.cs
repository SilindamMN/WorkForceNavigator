namespace Domain.Enties.Work
{
    using Domain.Enties.Hr;
    using Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Project : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public int TeamId { get; set; }

        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Client? Client { get; set; }
        public virtual Team? Team { get; set; }

        public ICollection<TimesheetEntry> TimesheetEntries { get; set; } = new List<TimesheetEntry>();
    }
}