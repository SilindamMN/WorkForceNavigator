﻿namespace Application.Services.Auth
{
  using Application.Helpers;
  using Application.Interfaces;
  using Application.Interfaces.Auth;
  using Domain.Account;
  using Domain.Constants;
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using Domain.Enums;
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

    public async Task<UserInfoResult> GetUserDetailsByUserNamesync(string userName)
    {
      var user = await userManager.FindByNameAsync(userName);
      if (user is null) { return null; }

      var roles = await userManager.GetRolesAsync(user);
      var userInfor = GenerateUserInfoObject(user, roles);
      return (UserInfoResult)userInfor;
    }

    public async Task<IEnumerable<UserInfoResult>> GetUserListAsync()
    {
      var users = await userManager.Users.ToListAsync();
      List<UserInfoResult> userInfoResults = new List<UserInfoResult>();

      foreach (var user in users)
      {
        var roles = await userManager.GetRolesAsync(user);
        var userInfo = GenerateUserInfoObject(user, roles);
        userInfoResults.Add((UserInfoResult)userInfo);
      }
      return userInfoResults;
    }

    public async Task<IEnumerable<string>> GetUsernamesListAsync()
    {
      return await userManager.Users.Select(u => u.UserName).ToListAsync();
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

      await logService.SaveNewLog(user.UserName, "New Login");
      return new LoginServiceResponseDto { NewToken = newToken, UserInfo = (UserInfoResult)userInfo };
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
      await logService.SaveNewLog(user.UserName, "New Token Generated");

      return new LoginServiceResponseDto { NewToken = newToken, UserInfo = (UserInfoResult)userInfo };
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
      //USER IS THE OWNER
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

    private object GenerateUserInfoObject(ApplicationUser user, IList<string> roles)
    {
      return new UserInfoResult
      {
        Id = user.Id,
        CreatedAt = DateTime.Now,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Roles = roles,
        Username = user.UserName,
        PhoneNumber = user.PhoneNumber,
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
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Roles = roles,
        Username = user.UserName,
        LineManager = user.LineManager,
        Salary = user.Salary,
        Department = details.DepartmentName,
        JobTitle = details.Title,
        PhoneNumber = user.PhoneNumber,
        Gender = (Gender)user.Gender,
      };
      return userInfo;
    }

    public async Task<GeneralServiceResponseDto> UpdateUserDetails(string username, UpdateUserDetailsDto userDetailsDto)
    {
      // Step 1: Retrieve the user from the database
      var user = await dataContext.Users
          .Include(u => u.JobTitle)
          .Where(x => x.UserName == username)
          .FirstOrDefaultAsync();

      if (user == null)
      {
        // Handle the case where the user is not found
        throw new Exception("User not found");
      }

      // Step 2: Update the user entity with the new values
      user.FirstName = userDetailsDto.FirstName;
      user.LastName = userDetailsDto.LastName;
      user.Gender = userDetailsDto.Gender;
      user.Salary = userDetailsDto.Salary;
      user.PhoneNumber = userDetailsDto.Phonenumber;
      user.JobTitle.Title = userDetailsDto.JobTitle; // Assuming JobTitle has a Title property

      // Step 3: Save the changes to the database
      await dataContext.SaveChangesAsync();

      // Return a response indicating success
      return new GeneralServiceResponseDto { };
    }
  }
}