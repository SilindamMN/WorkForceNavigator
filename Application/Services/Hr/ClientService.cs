namespace Application.Services.Hr
{
    using Persistence;
    using System.Threading.Tasks;
    using System.Linq;
    using Application.Dtos.Hr.Clients;
    using Application.Interfaces.Hr;

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