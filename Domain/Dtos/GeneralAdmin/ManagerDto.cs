using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Account;
using Domain.Enties.TimeSheets;
using Domain.Entities;

namespace Domain.Dtos.GeneralAdmin
{
    public class ManagerDto
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string TeamName { get; set; }
    }
}