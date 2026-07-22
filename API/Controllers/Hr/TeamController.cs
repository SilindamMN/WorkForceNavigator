namespace API.Controllers.Hr
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Dtos.Hr.Teams;
    using Application.Interfaces;
    using Application.Interfaces.Hr;
    using Application.Services.Auth;
    using Domain.Enties.Hr;
    using Domain.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly IGenericService<Team, TeamDto> _teamService;
        private readonly ITeamService teamInterface;

        public TeamController(
            IGenericService<Team, TeamDto> teamService, ITeamService teamInterface)
        {
            _teamService = teamService;
            this.teamInterface = teamInterface;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var result = await _teamService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var result = await _teamService.GetByIdAsync(id);
            if (result is null)
            {
                return NotFound("Team not found");
            }
            return Ok(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTeamByUserIdAsync(string userId)
        {
            var result = await teamInterface.GetTeamByUserIdAsync(userId);
            if (result is null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] TeamDto teamDto)
        {
            var result = await teamInterface.CreateTeam(teamDto);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamDto updateTeamDto)
        {
            var result = await _teamService.UpdateAsync(id, updateTeamDto);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteTeam(int id)
        {
            var result = await _teamService.SoftDeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}/undo")]
        public async Task<IActionResult> UnSoftDeleteTeam(int id)
        {
            var result = await _teamService.UndoSoftDeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPost("addmember")]
        public async Task<IActionResult> AddTeamMember(CreateUserTeamDto createUserTeamDto)
        {
            var response = await teamInterface.UpdateTeamMembership(createUserTeamDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("remove-member")]
        public async Task<IActionResult> RemoveTeamMember(CreateUserTeamDto createUserTeamDto)
        {
            var response = await teamInterface.UpdateTeamMembership(createUserTeamDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTeamsByDepartmentIdAsync([FromQuery] int departmentId)
        {
            var result = await teamInterface.GetAvailableTeamsByDepartmentIdAsync(departmentId);

            if (!result.Any())
            {
                return NotFound("No available teams found for this user.");
            }

            return Ok(result);
        }

        [HttpGet("withdetails")]
        public async Task<ActionResult<IEnumerable<TeamMemberDetailsDto>>> GetAllTeamsWithMembers()
        {
            var teams = await teamInterface.GetAllTeamsWithMembersAsync();
            return Ok(teams);
        }
    }
}