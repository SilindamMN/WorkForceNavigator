using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Account;
using Domain.Enties.TimeSheets;
using Domain.Entities;

namespace Domain.Enties
{
    public class Manager : BaseEntity<int>
    {
        public string ManagerId { get; set; } = null!;
        public ApplicationUser ManagerUser { get; set; } = null!;
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}