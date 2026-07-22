namespace Application.Dtos.Hr.JobTitles
{
    using Domain.Constants.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class UpdateCreateJobTitleDto
  {
    public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    public Seniority Seniority { get; set; }
    public int DepartmentId { get; set; }
  }
}