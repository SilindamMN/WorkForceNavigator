namespace Application.Dtos.Hr.Clients
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class ClientDto
  {
    public int Id { get; set; }
    public string ClientName { get; set; } = string.Empty.ToString();
    public string Phone { get; set; } = string.Empty.ToString();
    public string Fax { get; set; } = string.Empty.ToString();
    public string Email { get; set; } = string.Empty.ToString();
  }
}