namespace Application.Interfaces.Hr
{
    using Application.Dtos.Hr.Clients;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IClientService
  {
    Task<IEnumerable<ClientDetailDto>> GetClientProjectAsync(int id);
  }
}