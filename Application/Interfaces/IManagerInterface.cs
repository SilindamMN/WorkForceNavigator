namespace Application.Interfaces
{
    using Domain.Dtos.GeneralAdmin;
    using Domain.Dtos.LeaveTypes.Teams;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IManageInterface
    {
        Task<IEnumerable<ManagerDto>> GetAllManagers();
        Task<ManagerDto?> GetManagerByIdAsync(string userId);
    }
}