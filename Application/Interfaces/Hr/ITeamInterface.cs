namespace Application.Interfaces.Hr
{
    using Application.Dtos.Hr.Teams;
    using Domain.Dtos.General;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITeamInterface
  {
    Task<GeneralServiceResponseDto> UpdateTeamMembership(CreateUserTeamDto  createUserTeamDto);
    Task<IEnumerable<TeamMemberDetailsDto>> GetAllTeamsWithMembersAsync();
    Task<GeneralServiceResponseDto> CreateTeam(TeamDto team);
        Task<IEnumerable<UserTeamListDto>> GetTeamByUserIdAsync(string userId);
        Task<IEnumerable<UserTeamListApplicableDto>> GetAvailableTeamsByDepartmentIdAsync(int departmentId);

    }
}