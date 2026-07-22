namespace Application.Services.Auth
{
    using Application.Dtos.Account.Users;
    using Application.Helpers;
    using Application.Interfaces.Auth;
    using Application.Interfaces.Leaves;
    using Domain.Account;
    using Domain.Constants;
    using Domain.Constants.Enums;
    using Domain.Dtos.General;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
  {
    private readonly DataContext dataContext;
    private readonly IUserJobTitleService userJobTitleService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration configuration;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ILogService logService;
    private readonly ILeaveAllocationService leaveAllocationService;

    public AuthService(DataContext dataContext, IUserJobTitleService userJobTitleService, UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, ILogService logService, ILeaveAllocationService leaveAllocationService)
    {
      this.dataContext = dataContext;
      this.userJobTitleService = userJobTitleService;
      this.userManager = userManager;
      this.configuration = configuration;
      this.roleManager = roleManager;
      this.logService = logService;
      this.leaveAllocationService = leaveAllocationService;
    }

    public async Task<UserInfoResult?> GetUserDetailsByUserNamesync(string userName)
    {
      var user = await userManager.FindByNameAsync(userName);
      if (user is null) { return null; }

      var roles = await userManager.GetRolesAsync(user);
      var userInfor = await GenerateUserInfoObject(user, roles);
      return userInfor;
    }

    public async Task<IEnumerable<UserInfoResult>> GetUserListAsync()
    {
            var users = await userManager.Users
        .Include(u => u.JobTitle)
            .ThenInclude(jt => jt.Department)
        .Include(u => u.UserTeams)
            .ThenInclude(ut => ut.Team)
                .ThenInclude(t => t.Department)
        .ToListAsync();

            List<UserInfoResult> userInfoResults = new List<UserInfoResult>();

      foreach (var user in users)
      {
        var roles = await userManager.GetRolesAsync(user);
        var userInfo = await GenerateUserInfoObject(user, roles);
        userInfoResults.Add(userInfo);
      }
      return userInfoResults;
    }

    public async Task<IEnumerable<string>> GetUsernamesListAsync()
    {
      return await userManager.Users.Select(u => u.UserName).ToListAsync() ;
    }

    public async Task<LoginServiceResponseDto> LoginAsync(LoginDto loginDto)
    {
      var user = await userManager.FindByNameAsync(loginDto.Username);
      if (user is null)
      {
        return null;
      }
      var isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginDto.Password);
      if (!isPasswordCorrect)
      {
        return null;
      }
      var newToken = await GenerateJWTTokenAsync(user);
      var roles = await userManager.GetRolesAsync(user);
      var userInfo = GenerateUserInfoObject(user, roles);

     // await logService.SaveNewLog(user.UserName, "New Login");
      return new LoginServiceResponseDto { NewToken = newToken, UserInfo = await userInfo };
    }

    public async Task<LoginServiceResponseDto> MeAsync(MeDto meDto)
    {
      ClaimsPrincipal handler = new JwtSecurityTokenHandler().ValidateToken(meDto.Token, new TokenValidationParameters()
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidAudience = configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
      }, out SecurityToken securityToken);

      string decodeUsername = handler.Claims.First(x => x.Type == ClaimTypes.Name).Value;
      if (decodeUsername is null) { return null; }

      var user = await userManager.FindByNameAsync(decodeUsername);
      if (user is null) { return null; }

      var newToken = await GenerateJWTTokenAsync(user);
      var roles = await userManager.GetRolesAsync(user);
      var userInfo = GenerateUserInfoObject(user, roles);
      await logService.SaveNewLog(user?.UserName ?? string.Empty, "New Token Generated");

      return new LoginServiceResponseDto { NewToken = newToken, UserInfo = await userInfo };
    }

    public async Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto)
    {
      var userExists = await userManager.FindByEmailAsync(registerDto.Email);
      if (userExists is not null)
      {
        ResponseHelper.CreateResponse(false, 409, "User Already Exist");
      }
      ApplicationUser user = new ApplicationUser()
      {
        Email = registerDto.Email,
        FirstName = registerDto.FirstName,
        LastName = registerDto.LastName,
        UserName = registerDto.Username,
        SecurityStamp = Guid.NewGuid().ToString(),
      };
      var createUserResult = await userManager.CreateAsync(user, registerDto.Password);
      if (!createUserResult.Succeeded)
      {
        var errorString = "Failed to create user";
        foreach (var error in createUserResult.Errors)
        {
          errorString += " # " + error.Description;
        }
        return ResponseHelper.CreateResponse(false, 400, errorString);
      }
      await userManager.AddToRoleAsync(user, StaticUserRoles.USER);
      await logService.SaveNewLog(user.UserName, "Registed");
      await leaveAllocationService.CreateLeaveAllocation(registerDto.Username);
      return ResponseHelper.CreateResponse(true, 201, "User Registed Successfully");
    }

    public async Task<GeneralServiceResponseDto> SeedRolesAsync()
    {
      bool isOwnerRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
      bool isAdminRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
      bool isManagerRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.MANAGER);
      bool isUserRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.USER);

      if (isOwnerRoleExists && isAdminRoleExists && isManagerRoleExists && isUserRoleExists)
      {
        return ResponseHelper.CreateResponse(true, 200, "Roles Seeding Done Successfully");
      }

      await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
      await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
      await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.MANAGER));
      await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));

      return ResponseHelper.CreateResponse(true, 200, "Roles Seeding Done Successfully");
    }

    public async Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal User, UpdateRoleDto updateRoleDto)
    {
      var user = await userManager.FindByNameAsync(updateRoleDto.Username);
      if (user is null)
      {
        return ResponseHelper.CreateResponse(false, 404, "Invalid UserName");
      }
      var userRoles = await userManager.GetRolesAsync(user);
      //admin and owner can change
      if (User.IsInRole(StaticUserRoles.ADMIN))
      {
        //user is admin
        if (updateRoleDto.NewRole == RoleType.USER || updateRoleDto.NewRole == RoleType.MANAGER)
        {
          //admin can change role for everyone except for owners and admins
          if (userRoles.Any(r => r.Equals(StaticUserRoles.OWNER) || r.Equals(StaticUserRoles.OWNER)))
          {
            return ResponseHelper.CreateResponse(false, 403, "You not allowed to change role for this user");
          }
          else
          {
            await userManager.RemoveFromRolesAsync(user, userRoles);
            await userManager.AddToRoleAsync(user, updateRoleDto.NewRole.ToString());
            await logService.SaveNewLog(user.UserName, "Role update");
            return ResponseHelper.CreateResponse(true, 200, "Role Updated Successfully");
          }
        }
        else return ResponseHelper.CreateResponse(false, 403, "You are not allowed to change role of this user");
      }

      else
      {
        if (userRoles.Any(x => x.Equals(StaticUserRoles.OWNER)))
        {
          return ResponseHelper.CreateResponse(false, 403, "You are not allowed to change role of this user");
        }
        else
        {
          await userManager.RemoveFromRolesAsync(user, userRoles);
          await userManager.AddToRoleAsync(user, updateRoleDto.NewRole.ToString());
          await logService.SaveNewLog(user.UserName, "Role update");
          return ResponseHelper.CreateResponse(true, 200, "Role Updated Successfully");
        }
      }
    }

    private async Task<UserInfoResult> GenerateUserInfoObject(ApplicationUser user, IList<string> roles)
    {
            var currentUserTeam = await dataContext.UserTeams
           .Where(ut => ut.UserId == user.Id)
           .Select(ut => new
           {
               ut.TeamId,
               TeamName = ut.Team.TeamName ?? string.Empty,
               DepartmentId = ut.Team.DepartmentId,
               DepartmentName = ut.Team.Department.DepartmentName
           })
           .FirstOrDefaultAsync();

            var userTitle = await userJobTitleService.GetJobTitleForUser(user.UserName);
             
            return new UserInfoResult
      {
        Id = user.Id,
        CreatedAt = DateTime.Now,
        Email = user.Email ?? string.Empty,
        FirstName = user.FirstName,
        LastName = user.LastName,
                TeamId = currentUserTeam?.TeamId,
                TeamName = currentUserTeam?.TeamName ?? string.Empty,
                DepartmentId = currentUserTeam?.DepartmentId,
                DepartmentName = currentUserTeam?.DepartmentName ?? string.Empty,
                JobTitleId = user.JobTitleId,
                JobTitleName = userTitle?.Title ?? string.Empty ,
          Roles = roles,
        Seniority = user.Seniority,
        Username = user.UserName,
        PhoneNumber = user.PhoneNumber ?? string.Empty,
        Gender = (Gender?)user.Gender
      };
    }

    private async Task<string> GenerateJWTTokenAsync(ApplicationUser applicationUser)
    {
      var userRoles = await userManager.GetRolesAsync(applicationUser);
      var authClaims = new List<Claim>()
      {
        new Claim(ClaimTypes.Name,applicationUser.UserName),
        new Claim(ClaimTypes.NameIdentifier,applicationUser.Id),
        new Claim("FirstName",applicationUser.UserName),
        new Claim("LastName",applicationUser.UserName),
      };
      foreach (var role in userRoles)
      {
        authClaims.Add(new Claim(ClaimTypes.Role, role));
      }
      var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
      var signinCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

      var tokenObject = new JwtSecurityToken(
        issuer: configuration["JWT:ValidIssuer"],
        audience: configuration["JWT:ValidAudience"],
        notBefore: DateTime.Now,
        expires: DateTime.Now.AddHours(9),
        claims: authClaims,
        signingCredentials: signinCredentials);

      string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
      return token;
    }

    public async Task<UserDetailsDto> GetUserExtraDetailsByUserNameAsync(string userName)
    {
      var user = await userManager.FindByNameAsync(userName);
      if (user is null) { return null; }

      var roles = await userManager.GetRolesAsync(user);
      var userInfor = await GenerateUserInfo(user, roles, userJobTitleService);
      return userInfor;
    }

    private async Task<UserDetailsDto> GenerateUserInfo(ApplicationUser user, IList<string> roles, IUserJobTitleService userJobTitleService)
    {
      var details = await userJobTitleService.GetJobTitleForUser(user.UserName);
      var userInfo = new UserDetailsDto
      {
        Id = user.Id,
        JoiningDate = user.CreatedAt,
        Email = user.Email ?? string.Empty,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Roles = roles,
        Username = user.UserName,
        Salary = user.Salary,
          DepartmentId = user.JobTitle?.DepartmentId,
          Department = details.DepartmentName ?? string.Empty,
          JobTitleId = user.JobTitleId,
        JobTitle = details.Title,
        TeamId = user.UserTeams.FirstOrDefault()?.TeamId,
          TeamName = user.UserTeams?.FirstOrDefault()?.Team?.TeamName ?? string.Empty,
          PhoneNumber = user?.PhoneNumber ?? string.Empty,
          Gender = (Gender)user.Gender,
      };
      return userInfo;
    }

    public async Task<GeneralServiceResponseDto> UpdateUserDetails(string username,int departmentId, UpdateUserDetailsDto userDetailsDto)
    {
      var user = await dataContext.Users
          .Include(u => u.JobTitle)
          .Where(x => x.UserName == username)
          .FirstOrDefaultAsync();

      if (user == null)
      {
        throw new Exception("User not found");
      }
            var jobTitles = await userJobTitleService.GetJobTitleByDepartmentAndSeniorityAsync(departmentId,user.Seniority);

            var validJobTitle = jobTitles.Any(j => j.JobTitleId == userDetailsDto.JobTitleId);

            if (!validJobTitle)
            {
                throw new Exception("Invalid Job Title for this department");
            }

            user.FirstName = userDetailsDto.FirstName;
      user.LastName = userDetailsDto.LastName;
      user.Gender = userDetailsDto.Gender;
      user.Salary = userDetailsDto.Salary;
      user.PhoneNumber = userDetailsDto.Phonenumber;
            user.JobTitleId = userDetailsDto.JobTitleId; 
            user.Seniority = Enum.Parse<Seniority>(
    jobTitles.First(x => x.JobTitleId == userDetailsDto.JobTitleId).Seniority);
            await dataContext.SaveChangesAsync();

      return new GeneralServiceResponseDto { };
    }
  }
}