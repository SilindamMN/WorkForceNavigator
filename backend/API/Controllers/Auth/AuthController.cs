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

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        private readonly IUserJobTitleService userJobTitleService;

        public AuthController(IAuthService authService, IUserService userService, IUserJobTitleService userJobTitleService)
        {
            this.authService = authService;
            this.userService = userService;
            this.userJobTitleService = userJobTitleService;
        }

        [HttpGet]
        [Authorize(Roles = StaticUserRoles.USER)]
        [Route("jobtitle/{username}")]
        public async Task<ActionResult<JobTitleDto>> GetJobTitleByUsername([FromRoute] string username)
        {
            var user = await userJobTitleService.GetJobTitleForUserAsync(username);
            if (user is not null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Username not found");
            }
        }

        [HttpPost]
        [Route("seed-roles")]
        [Authorize(Roles =StaticUserRoles.ADMIN)]
        public async Task<IActionResult> SeedRoles()
        {
            var seedResults = await authService.SeedRolesAsync();
            return StatusCode(seedResults.StatusCode, seedResults.Message);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var registerResult = await authService.RegisterAsync(registerDto);
            return StatusCode(registerResult.StatusCode, new
            {
                message = registerResult.Message
            });
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResult = await authService.LoginAsync(loginDto);
            if (loginResult is null)
            {
                return Unauthorized("Invalid Credentials");
            }
            return Ok(loginResult);
        }
        [HttpPatch]
        [Authorize(Roles =StaticUserRoles.ADMIN)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto updateRoleDto)
        {
            var updateRoleResult = await authService.UpdateRoleAsync(User, updateRoleDto);
            if (updateRoleResult.IsSucceed)
            {
                return Ok(updateRoleResult.Message);
            }
            else
            {
                return StatusCode(updateRoleResult.StatusCode, updateRoleResult.Message);
            }
        }

        [HttpGet("me")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<ActionResult<LoginServiceResponseDto>> Me([FromBody] MeDto token)
        {
            var me = await authService.MeAsync(token);
            if (me is null)
            {
                return Ok(me);
            }
            else
            {
                return Unauthorized("Invalid Token");
            }
        }

        [HttpGet]
        [Route("{username}")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetailsByUsernames([FromRoute] string username)
        {
            var user = await userService.GetUserDetailsByUserNameAsync(username);

            if (user is not null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Username not found");
            }
        }
    }
}