namespace Domain.Dtos.GeneralAdmin
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class ClientDto
  {
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string Email { get; set; }
  }
}