namespace Application.Dtos.Hr.Teams
{
    using Domain.Account;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TeamDto
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}