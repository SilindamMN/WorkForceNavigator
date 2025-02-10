namespace Application.Services.GenericServices
{
  using Application.Interfaces.GenericInterfaces;
  using Persistence;
  using System.Threading.Tasks;
  using System.Linq;
  using Domain.Dtos.GeneralAdmin;

  public class ClientService : IClientService
  {
    private readonly DataContext dataContext;

    public ClientService(DataContext dataContext)
    {
      this.dataContext = dataContext;
    }

    public async Task<IEnumerable<ClientDetailDto>> GetClientProjectAsync(int clientId)
    {
      var projectNames = (from p in dataContext.Projects
                          where p.ClientId == clientId
                          select new ClientDetailDto
                          {
                            ProjectName = p.ProjectName
                          }).ToList();
      return projectNames;
    }
  }
}