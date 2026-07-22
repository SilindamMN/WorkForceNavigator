namespace Application.Services.Hr
{
    using Application.Helpers;
    using Domain.Dtos.General;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Domain.Account;
    using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
    using Microsoft.EntityFrameworkCore;
    using Application.Dtos.Hr.Teams;
    using Application.Interfaces.Hr;
    using Domain.Enties.Hr;
    using Microsoft.AspNetCore.Http;

    public class TeamService : ITeamService
    {
        private readonly DataContext dataContext;
        private readonly UserManager<ApplicationUser> userManager;

        public TeamService(DataContext dataContext, UserManager<ApplicationUser> userManager)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
        }

        public async Task<GeneralServiceResponseDto> UpdateTeamMembership(CreateUserTeamDto dto)
        {
                var user = await userManager.FindByIdAsync(dto.UserId);
                if (user == null)
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "User not found.");

                var team = await dataContext.Teams
                    .FirstOrDefaultAsync(x => x.Id == dto.TeamId);
                if (team == null)
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Team not found.");

                if (dto.IsRemove)
                {
                    var userTeams = await dataContext.UserTeams
                        .Where(x => x.UserId == dto.UserId && x.TeamId == dto.TeamId)
                        .ToListAsync();
                    dataContext.UserTeams.RemoveRange(userTeams);
                    dataContext.Users.Update(user);
                    await dataContext.SaveChangesAsync();
                    return ResponseHelper.CreateResponse(true, StatusCodes.Status200OK, "Member removed successfully.");
                }

                if (await IsUserInTeam(dto.UserId, dto.TeamId))
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "User already in this team.");

                if (await IsUserInMaxTeams(dto.UserId))
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "User already in max 3 teams.");

                var userTeam = new UserTeam
                {
                    UserId = dto.UserId,
                    TeamId = dto.TeamId,
                };

                dataContext.UserTeams.Add(userTeam);
                await dataContext.SaveChangesAsync();
                return ResponseHelper.CreateResponse(true, StatusCodes.Status201Created, "Member added successfully.");
            
        }
        public async Task<IEnumerable<TeamMemberDetailsDto>> GetAllTeamsWithMembersAsync()
        {
                var teamsWithMembers = await (from ut in dataContext.UserTeams
                                              join u in dataContext.Users on ut.UserId equals u.Id
                                              join t in dataContext.Teams on ut.TeamId equals t.Id
                                              join jt in dataContext.JobTitles on u.JobTitleId equals jt.Id into jobTitles
                                              from jt in dataContext.JobTitles.DefaultIfEmpty()
                                              select new
                                              {
                                                  TeamName = t.TeamName,
                                                  Member = new MemberDetails
                                                  {
                                                      FirstName = u.FirstName,
                                                      LastName = u.LastName,
                                                      JobTitle = jt.Title
                                                  }
                                              }).ToListAsync();

                var teams = teamsWithMembers.GroupBy(t => new { t.TeamName, })
                                            .Select(g => new TeamMemberDetailsDto
                                            {
                                                TeamLeader = g.Key.TeamName,
                                                TeamName = g.Key.TeamName,
                                                MemberDetails = g.Select(m => m.Member).ToList()
                                            }).ToList();
                return teams;
        }

        public async Task<GeneralServiceResponseDto> CreateTeam(TeamDto teamDto)
        {
                var existingTeam = dataContext.Teams.FirstOrDefault(t => t.TeamName == teamDto.TeamName);

                if (existingTeam != null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "A team with this name already exists.");
                }
                var teamLeader = await userManager.FindByNameAsync(teamDto.TeamLeader);

                if (teamLeader == null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "User Name not found or invalid.");
                }

                if (!await IsUserManager(teamDto.TeamLeader))
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status403Forbidden, "The assigned user does not qualify to be team leader");
                }
                if (await IsUserTeamLeaderInAnyTeam(teamDto.TeamLeader))
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status400BadRequest, "User is already a team leader of another team.");
                }

                var newTeam = new Team
                {
                    TeamName = teamDto.TeamName,
                    Description = teamDto.Description
                };

                await dataContext.Teams.AddAsync(newTeam);
                await dataContext.SaveChangesAsync();

                var createdTeam = await dataContext.Teams.FindAsync(newTeam.Id);

                if (createdTeam == null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status500InternalServerError, "Failed to retrieve the newly created team.");
                }

                var userId = await GetUserIdAsync(teamDto.TeamLeader);

                if (userId == null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "User not found.");
                }

                var newUserTeamRecord = new UserTeam
                {
                    UserId = userId, // Assuming TeamLeader is the userId
                    TeamId = createdTeam.Id
                };
                await dataContext.UserTeams.AddAsync(newUserTeamRecord);

                await dataContext.SaveChangesAsync();

            return ResponseHelper.CreateResponse(true, StatusCodes.Status201Created, "Team created successfully.");   
        }
        private async Task<bool> IsUserManager(string username)
        {
                // Retrieve the user by username
                var user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    // User not found
                    return false;
                }

                // Check if the user has the Manager role
                return await userManager.IsInRoleAsync(user, "Manager");
        }

        // Private method to check if the user is already a member of the maximum allowed teams
        private async Task<bool> IsUserInMaxTeams(string userId)
        {
            return await dataContext.UserTeams
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.TeamId)
                .Distinct()
                .CountAsync() >= 3;
        }

        // Private method to check if the user is already a team leader in any team
        private async Task<bool> IsUserTeamLeaderInAnyTeam(string username)
        {
            // Count the number of teams the user is a team leader of
            var teamLeaderCount = await dataContext.Teams.CountAsync(t => t.TeamName == username);
            return teamLeaderCount > 0;
        }

        private async Task<bool> IsUserInTeam(string userId, int teamId)
        {
            return await dataContext.UserTeams
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.TeamId == teamId);
        }
        private async Task<string> GetUserIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                user = await userManager.FindByIdAsync(userId);
            }
            return user.Id;
        }

        public async Task<IEnumerable<UserTeamListDto>> GetTeamByUserIdAsync(string userId)
        {
            var teams = await (
                from ut in dataContext.UserTeams
                where ut.UserId == userId
                join user in dataContext.Users
                    on ut.UserId equals user.Id
                join t in dataContext.Teams
                    on ut.TeamId equals t.Id

                select new UserTeamListDto
                {
                    Id = t.Id,
                    UserName = $"{user.FirstName} {user.LastName}",
                    TeamName = t.TeamName
                })
                .ToListAsync();

            return teams;
        }

        public async Task<IEnumerable<UserTeamListApplicableDto>> GetAvailableTeamsByDepartmentIdAsync(int departmentId)
        {
            return await dataContext.Teams
                .AsNoTracking()
                .Where(x => x.DepartmentId == departmentId)
                 .Select(x => new UserTeamListApplicableDto
                 {
                     Id = x.Id,
                     TeamName = x.TeamName
                 }).ToListAsync();
        }
    }
}