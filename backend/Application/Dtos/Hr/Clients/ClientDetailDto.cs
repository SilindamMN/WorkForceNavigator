namespace Application.Dtos.Hr.Clients
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

    public class ClientDetailsDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public List<string> ProjectNames { get; set; } = new();
    }
}