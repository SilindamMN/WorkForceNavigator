using Application.Dtos.Account.Users;
using Application.Helpers;
using Application.Interfaces.Auth;
using Domain.Account;
using Domain.Constants.Enums;
using Domain.Dtos.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DataContext dataContext;
        private readonly IUserJobTitleService userJobTitleService;

        public UserService(UserManager<ApplicationUser> userManager, DataContext dataContext, IUserJobTitleService userJobTitleService)
        {
            this.userManager = userManager;
            this.dataContext = dataContext;
            this.userJobTitleService = userJobTitleService;
        }

        public  Task<UserDetailsDto> GetUserExtraDetailsByUserNameAsync(string userName)
        {
            var user =  userManager.FindByNameAsync(userName).Result;
            if (user is null) { return null; }

            var roles =  userManager.GetRolesAsync(user).Result;
            var userInfor =  GenerateUserInfo(user, roles, userJobTitleService);
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
                var userInfo = await GenerateUserInfoAsync(user, roles);
                userInfoResults.Add(userInfo);
            }
            return userInfoResults;
        }

        public async Task<IEnumerable<string>> GetUsernamesListAsync()
        {
            return await userManager.Users.Select(u => u.UserName).ToListAsync();
        }

        public async Task<UserInfoResult> GenerateUserInfoAsync(ApplicationUser user, IList<string> roles)
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

            var userTitle = await userJobTitleService.GetJobTitleForUserAsync(user.UserName);

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
                JobTitleId = user?.JobTitleId,
                JobTitleName = userTitle?.Title ?? string.Empty,
                Roles = roles,
                Seniority = user?.Seniority,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Gender = (Gender?)user.Gender
            };
        }
        public Task<UserInfoResult> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = userManager.FindByNameAsync(userName).Result;
            if (user is null) { return null; }

            var roles = userManager.GetRolesAsync(user).Result;
            var userInfor = GenerateUserInfoAsync(user, roles);
            return userInfor;
        }

        public async Task<GeneralServiceResponseDto> UpdateUserDetailsAsync(string username, int departmentId, UpdateUserDetailsDto userDetailsDto)
        {
            var user = await dataContext.Users
                .Include(u => u.JobTitle)
                .Where(x => x.UserName == username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "User Not Found");
            }
            var jobTitles = await userJobTitleService.GetJobTitleByDepartmentAndSeniorityAsync(departmentId, user.Seniority);

            var validJobTitle = jobTitles.Any(j => j.JobTitleId == userDetailsDto.JobTitleId);

            if (!validJobTitle)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status412PreconditionFailed, "Invalid Job Title for this department");
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
        private async Task<UserDetailsDto> GenerateUserInfo(ApplicationUser user, IList<string> roles, IUserJobTitleService userJobTitleService)
        {
            var details = await userJobTitleService.GetJobTitleForUserAsync(user.UserName);
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
    }
}