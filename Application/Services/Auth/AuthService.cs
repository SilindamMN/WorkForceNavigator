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
    using Microsoft.AspNetCore.Http;
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
        private readonly IUserService userService;
        private readonly IUserJobTitleService userJobTitleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogService logService;
        private readonly ILeaveAllocationService leaveAllocationService;

        public AuthService(DataContext dataContext, IUserService userService, IUserJobTitleService userJobTitleService, UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, ILogService logService, ILeaveAllocationService leaveAllocationService)
        {
            this.dataContext = dataContext;
            this.userService = userService;
            this.userJobTitleService = userJobTitleService;
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.logService = logService;
            this.leaveAllocationService = leaveAllocationService;
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
            var userInfo = this.userService.GenerateUserInfoAsync(user, roles);

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
            var userInfo = this.userService.GenerateUserInfoAsync(user, roles);
            await logService.SaveNewLog(user?.UserName ?? string.Empty, "New Token Generated");

            return new LoginServiceResponseDto { NewToken = newToken, UserInfo = await userInfo };
        }

        public async Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var userExists = await userManager.FindByEmailAsync(registerDto.Email);
            if (userExists is not null)
            {
                ResponseHelper.CreateResponse(false, StatusCodes.Status409Conflict, "User Already Exist");
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
                return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, errorString);
            }
            await userManager.AddToRoleAsync(user, StaticUserRoles.USER);
            await logService.SaveNewLog(user.UserName, "Registed");
            await leaveAllocationService.CreateLeaveAllocation(registerDto.Username);
            return ResponseHelper.CreateResponse(true, StatusCodes.Status201Created, "User Registed Successfully");
        }

        public async Task<GeneralServiceResponseDto> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isManagerRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.MANAGER);
            bool isUserRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isManagerRoleExists && isUserRoleExists)
            {
                return ResponseHelper.CreateResponse(true, StatusCodes.Status200OK, "Roles Seeding Done Successfully");
            }

            await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
            await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.MANAGER));
            await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));

            return ResponseHelper.CreateResponse(true, StatusCodes.Status200OK, "Roles Seeding Done Successfully");
        }

        public async Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal User, UpdateRoleDto updateRoleDto)
        {
            var user = await userManager.FindByNameAsync(updateRoleDto.Username);
            if (user is null)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Invalid UserName");
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
                        return ResponseHelper.CreateResponse(false, StatusCodes.Status403Forbidden, "You not allowed to change role for this user");
                    }
                    else
                    {
                        await userManager.RemoveFromRolesAsync(user, userRoles);
                        await userManager.AddToRoleAsync(user, updateRoleDto.NewRole.ToString());
                        await logService.SaveNewLog(user.UserName, "Role update");
                        return ResponseHelper.CreateResponse(true, StatusCodes.Status200OK, "Role Updated Successfully");
                    }
                }
                else return ResponseHelper.CreateResponse(false, StatusCodes.Status403Forbidden, "You are not allowed to change role of this user");
            }

            else
            {
                if (userRoles.Any(x => x.Equals(StaticUserRoles.OWNER)))
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status403Forbidden, "You are not allowed to change role of this user");
                }
                else
                {
                    await userManager.RemoveFromRolesAsync(user, userRoles);
                    await userManager.AddToRoleAsync(user, updateRoleDto.NewRole.ToString());
                    await logService.SaveNewLog(user.UserName, "Role update");
                    return ResponseHelper.CreateResponse(true, StatusCodes.Status201Created, "Role Updated Successfully");
                }
            }
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
    }
}