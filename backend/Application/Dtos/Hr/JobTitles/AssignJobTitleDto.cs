namespace Application.Dtos.Hr.JobTitles
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class AssignJobTitleDto
  {
    public string username { get; set; } = string.Empty;
    public int jobTitleId { get; set; }
  }
}