namespace Application.Services.GenericServices
{
    using Application.Interfaces;
    using Domain.Dtos.GeneralAdmin;
    using Domain.Dtos.LeaveTypes.Teams;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ManagerService : IManageInterface
    {
        private readonly DataContext dataContext;

        public ManagerService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<IEnumerable<ManagerDto>> GetAllManagers()
        {
            var managers = await (from manager in dataContext.Managers
                                  join user in dataContext.Users on manager.ManagerId equals user.Id
                                  join team in dataContext.Teams on manager.TeamId equals team.Id
                                  select new ManagerDto
                                  {
                                      Id = manager.Id,
                                      FullName = user.FirstName + " " + user.LastName,
                                      TeamName = team.TeamName
                                  }).ToListAsync();
            return managers;
        }
        public async Task<ManagerDto?> GetManagerByIdAsync(string userId)
        {
            return await (
                from manager in dataContext.Managers
                join user in dataContext.Users
                    on manager.ManagerId equals user.Id
                join team in dataContext.Teams
                    on manager.TeamId equals team.Id
                where user.Id == userId
                select new ManagerDto
                {
                    Id = manager.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    TeamName = team.TeamName
                })
                .FirstOrDefaultAsync();
        }
    }
}