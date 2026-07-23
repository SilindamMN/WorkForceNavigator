namespace Application.Dtos.Hr.Clients
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class CreateUpdateClientDto
    {
    public string? ClientName { get; set; } 
    public string? Phone { get; set; } 
    public string? Fax { get; set; } 
    public string? Email { get; set; } 
  }
}