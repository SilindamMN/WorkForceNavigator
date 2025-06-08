namespace API.Controllers.Auth
{
  using Application.Interfaces;
  using Application.Interfaces.Auth;
  using Application.Services.Auth;
  using Domain.Constants;
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using Domain.Dtos.JobTitles;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Persistence;

  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService authService;
    private readonly IUserJobTitleService userJobTitleService;

    public AuthController(IAuthService authService, IUserJobTitleService userJobTitleService)
    {
      this.authService = authService;
      this.userJobTitleService = userJobTitleService;
    }

    [HttpGet]
    [Route("jobtitle/{username}")]
    public async Task<ActionResult<JobTitleDto>> GetJobTitleByUsername([FromRoute] string username)
    {
      var user = await userJobTitleService.GetJobTitleForUser(username);
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
    public async Task<IActionResult> SeedRoles()
    {
      var seedResults = await authService.SeedRolesAsync();
      return StatusCode(seedResults.StatusCode, seedResults.Message);
    }

    [HttpPost]
    [Route("register")]
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
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      var loginResult = await authService.LoginAsync(loginDto);
      if (loginResult is null)
      {
        return Unauthorized("Invalid Credentials");
      }
      return Ok(loginResult);
    }

    [HttpPost]
    [Route("UpdateRoles")]
    //[Authorize(Roles =StaticUserRoles.OwnerAdmin)]
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

    [HttpPost]
    [Route("me")]
    public async Task<ActionResult<LoginServiceResponseDto>> Me([FromBody] MeDto token)
    {
      try
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
      catch (Exception ex)
      {
        return Unauthorized("Invalid Token");
      }
    }

    [HttpGet]
    [Route("users")]
    public async Task<ActionResult<IEnumerable<UserInfoResult>>> GetUsersList()
    {
      var userList = await authService.GetUserListAsync();
      return Ok(userList);
    }

    //[HttpGet]
    //[Route("users/{username}")]
    //public async Task<ActionResult<UserDetailsDto>> GetUserDetailsByUsernames([FromRoute] string username)
    //{
    //  var user = await authService.GetUserDetailsByUserNamesync(username);
    //  if (user is not null)
    //  {
    //    return Ok(user);
    //  }
    //  else
    //  {
    //    return NotFound("Username not found");
    //  }
    //}

    [HttpGet]
    [Route("userDetails/{username}")]
    public async Task<ActionResult<UserDetailsDto>> GetUserExtraDetailsByUsername([FromRoute] string username)
    {
      var user = await authService.GetUserExtraDetailsByUserNameAsync(username);
      if (user is not null)
      {
        return Ok(user);
      }
      else
      {
        return NotFound("Username not found");
      }
    }
    [HttpGet]
    [Route("usernames")]
    public async Task<ActionResult<IEnumerable<string>>> GetUsernameList()
    {
      var usernames = await authService.GetUsernamesListAsync();
      return Ok(usernames);
    }

    [HttpPost]
    [Route("update")]
    public async Task<ActionResult<GeneralServiceResponseDto>> UpdateUserDetails(string updateUsername, [FromBody] UpdateUserDetailsDto userDetailsDto)
    {
      try
      {
        // Assuming you have a service class that contains the UpdateUserDetails method
        var response = await authService.UpdateUserDetails(updateUsername, userDetailsDto);
        return Ok(response);
      }
      catch (Exception ex)
      {
        // Log the exception and return an error response
        return BadRequest(new GeneralServiceResponseDto { });
      }
    }
  }
}