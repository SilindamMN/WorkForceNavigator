namespace API.Controllers.Auth
{
    using Application.Dtos.Account.Users;
    using Application.Dtos.Hr.JobTitles;
    using Application.Interfaces.Auth;
    using Application.Services.Auth;
    using Domain.Constants;
    using Domain.Dtos.General;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Persistence;

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserJobTitleService userJobTitleService;

        public UsersController(IUserService userService, IUserJobTitleService userJobTitleService)
        {
            this.userService = userService;
            this.userJobTitleService = userJobTitleService;
        }

        [HttpGet]
        [Route("jobtitle/{username}")]
        public async Task<ActionResult<JobTitleDto>> GetJobTitleByUsername([FromRoute] string username)
        {
            var user = await userJobTitleService.GetJobTitleForUserAsync(username);
            return user is null ? NotFound("Username Not Found") : Ok(user);    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfoResult>>> GetUsersList()
        {
            var userList = await userService.GetUserListAsync();
            return Ok(userList);
        }

        [HttpGet]
        [Route("users/{username}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetailsByUsernames([FromRoute] string username)
        {
            var user = await userService.GetUserDetailsByUserNameAsync(username);
            return user is null ? NotFound("Username Not Found") : Ok(user);
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserExtraDetailsByUsername([FromRoute] string username)
        {
            var user = await userService.GetUserExtraDetailsByUserNameAsync(username);
            return user is null ? NotFound("Username Not Found") : Ok(user);
        }
        [HttpGet]
        [Route("usernames")]
        public async Task<ActionResult<IEnumerable<string>>> GetUsernameList()
        {
            var usernames = await userService.GetUsernamesListAsync();
            return Ok(usernames);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserDetails(string updateUsername, int departmentId, [FromBody] UpdateUserDetailsDto userDetailsDto)
        {
            var updateResult = await userService.UpdateUserDetailsAsync(updateUsername, departmentId, userDetailsDto);
            return StatusCode(updateResult.StatusCode,updateResult.Message);
        }
    }
}