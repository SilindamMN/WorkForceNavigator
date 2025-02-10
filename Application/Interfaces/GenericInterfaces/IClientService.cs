namespace Application.Interfaces.GenericInterfaces
{
  using Domain.Dtos.GeneralAdmin;
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