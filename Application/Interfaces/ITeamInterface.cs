namespace Application.Interfaces
{
  using Domain.Dtos.General;
  using Domain.Dtos.GeneralAdmin;
  using Domain.Dtos.LeaveTypes.Teams;
  using Domain.Entities.TimeSheets;
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
  }
}