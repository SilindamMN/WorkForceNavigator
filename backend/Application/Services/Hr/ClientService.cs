namespace Application.Services.Hr
{
    using Application.Dtos.Hr.Clients;
    using Application.Interfaces.Hr;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System.Linq;
    using System.Threading.Tasks;

    public class ClientService : IClientService
    {
        private readonly DataContext dataContext;

        public ClientService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ClientDetailsDto> GetClientDetailsAsync(int clientId)
        {
            var client = await dataContext.Clients
                .Where(c => c.Id == clientId)
                .Select(c => new ClientDetailsDto
                {
                    Id = c.Id,
                    ClientName = c.ClientName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Fax = c.Fax,
                    DepartmentId = c.DepartmentId ?? 0,
                    DepartmentName = c.Department != null ?
                    c.Department.DepartmentName : null,
                    ProjectNames = c.Projects.Select(p => p.ProjectName).ToList()
                }).FirstOrDefaultAsync();
            return client;
        }

    }
}