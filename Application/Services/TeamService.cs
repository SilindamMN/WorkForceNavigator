namespace Application.Services
{
  using Application.Helpers;
  using Application.Interfaces;
  using Domain.Dtos.General;
  using Domain.Dtos.GeneralAdmin;
  using Domain.Enties.TimeSheets;
  using Persistence;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Identity;
  using Domain.Account;
  using Domain.Dtos.LeaveTypes.Teams;
  using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
  using Microsoft.EntityFrameworkCore;
    using Application.Interfaces.GenericInterfaces;
    using Domain.Dtos.Account;

    public class TeamService : ITeamInterface
  {
    private readonly DataContext dataContext;
    private readonly UserManager<ApplicationUser> userManager;
        private readonly IManageInterface manageInterface;

        public TeamService(DataContext dataContext, UserManager<ApplicationUser> userManager,IManageInterface manageInterface)
    {
      this.dataContext = dataContext;
      this.userManager = userManager;
            this.manageInterface = manageInterface;
        }

        public async Task<GeneralServiceResponseDto> UpdateTeamMembership(CreateUserTeamDto dto)
        {
            try
            {
                var user = await userManager.FindByIdAsync(dto.UserId);
                if (user == null)
                    return ResponseHelper.CreateResponse(false, 400, "User not found.");

                var team = await dataContext.Teams
                    .FirstOrDefaultAsync(x => x.Id == dto.TeamId);
                if (team == null)
                    return ResponseHelper.CreateResponse(false, 400, "Team not found.");

                // REMOVE FLOW
                if (dto.IsRemove)
                {
                    var userTeams = await dataContext.UserTeams
                        .Where(x => x.UserId == dto.UserId && x.TeamId == dto.TeamId)
                        .ToListAsync();
                    dataContext.UserTeams.RemoveRange(userTeams);
                    user.LineManagerId = null;
                    dataContext.Users.Update(user);
                    await dataContext.SaveChangesAsync();
                    return ResponseHelper.CreateResponse(true, 200, "Member removed successfully.");
                }

                // ADD FLOW
                if (await IsUserInTeam(dto.UserId, dto.TeamId))
                    return ResponseHelper.CreateResponse(false, 400, "User already in this team.");

                if (await IsUserInMaxTeams(dto.UserId))
                    return ResponseHelper.CreateResponse(false, 400, "User already in max 3 teams.");

                var userTeam = new UserTeam
                {
                    UserId = dto.UserId,
                    TeamId = dto.TeamId,
                };

                // Only assign a line manager if the user doesn't already have one
                if (string.IsNullOrWhiteSpace(user.LineManagerId))
                {
                    var managerId = await manageInterface.GetManagerByTeamIdAsync(dto.TeamId);
                    if (managerId!=null)
                    {
                        return ResponseHelper.CreateResponse(false, 400, "No manager assigned to this team.");
                    }

                    var managerIdString = managerId?.ToString();

                    // Correct check: does this id actually exist as a User?
                    var managerExists = await dataContext.Users
                        .AnyAsync(u => u.Id == managerIdString);
                   

                    // Prevent a user being set as their own manager
                    if (managerIdString == user.Id)
                    {
                        return ResponseHelper.CreateResponse(false, 400, "A user cannot be their own line manager.");
                    }

                    user.LineManagerId = dto.UserId.ToString(); 
                    dataContext.Users.Update(user);
                }

                dataContext.UserTeams.Add(userTeam);
                await dataContext.SaveChangesAsync();
                return ResponseHelper.CreateResponse(true, 200, "Member added successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.CreateResponse(false, 500, ex.Message);
            }
        }
        public async Task<IEnumerable<TeamMemberDetailsDto>> GetAllTeamsWithMembersAsync()
    {
      try
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

        var teams = teamsWithMembers.GroupBy(t => new { t.TeamName,  })
                                    .Select(g => new TeamMemberDetailsDto
                                    {
                                      TeamLeader = g.Key.TeamName,
                                      TeamName = g.Key.TeamName,
                                      MemberDetails = g.Select(m => m.Member).ToList()
                                    }).ToList();

        return teams;
      }
      catch (Exception ex)
      {
        // Log the exception (optional)
        return Enumerable.Empty<TeamMemberDetailsDto>(); // Return an empty list in case of an error
      }
    }

    public async Task<GeneralServiceResponseDto> CreateTeam(TeamDto teamDto)
    {
      try
      {
        // Check if a team with the same name already exists
        var existingTeam = dataContext.Teams.FirstOrDefault(t => t.TeamName == teamDto.TeamName);

        if (existingTeam != null)
        {
          return ResponseHelper.CreateResponse(false, 400, "A team with this name already exists.");
        }

        // Check if the teamLeader is a Manager
        var teamLeader = await userManager.FindByNameAsync(teamDto.TeamLeader);

        if (teamLeader == null)
        {
          return ResponseHelper.CreateResponse(false, 400, "User Name not found or invalid.");
        }

        if (!await IsUserManager(teamDto.TeamLeader))
        {
          return ResponseHelper.CreateResponse(false, 403, "The assigned user does not qualify to be team leader");
        }

        // Check if the user is already a team leader of another team
        if (await IsUserTeamLeaderInAnyTeam(teamDto.TeamLeader))
        {
          return ResponseHelper.CreateResponse(false, 400, "User is already a team leader of another team.");
        }

        // Create a new team entity and populate it with data from the DTO
        var newTeam = new Team
        {
          TeamName = teamDto.TeamName,
          Description = teamDto.Description
        };

        // Add the new team to the data context
        await dataContext.Teams.AddAsync(newTeam);
        await dataContext.SaveChangesAsync();

        // Retrieve the newly created team from the database to get its Id
        var createdTeam = await dataContext.Teams.FindAsync(newTeam.Id);

        if (createdTeam == null)
        {
          return ResponseHelper.CreateResponse(false, 500, "Failed to retrieve the newly created team.");
        }

        // Fetch the user's ID based on the TeamLeader username/email provided in the TeamDto
        var userId = await GetUserIdAsync(teamDto.TeamLeader);

        if (userId == null)
        {
          return ResponseHelper.CreateResponse(false, 404, "User not found.");
        }

                var newUserTeamRecord = new UserTeam
                {
                    UserId = userId, // Assuming TeamLeader is the userId
          TeamId = createdTeam.Id
                };

        // Add the new UserTeam record to the data context
        await dataContext.UserTeams.AddAsync(newUserTeamRecord);

        // Save changes to the database
        await dataContext.SaveChangesAsync();


        // Return a success response
        return ResponseHelper.CreateResponse(true, 201, "Team created successfully.");
      }
      catch (Exception ex)
      {
        // Return an error response in case of an exception
        return ResponseHelper.CreateResponse(false, 500, $"An error occurred: {ex.Message}");
      }
    }

    private async Task<bool> IsUserManager(string username)
    {
      try
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
      catch (Exception ex)
      {
        // Log or handle the exception as needed
        // For simplicity, returning false if any exception occurs
        return false;
      }
    }

        // Fix for CS0029 and CS8603 in the AssignLineManager method
        //private async Task<string?> AssignLineManager(int teamId)
        //{
        //    try
        //    {
        //        // Retrieve the TeamLeader's username for the specified team
        //        var teamLeader = await dataContext.Teams
        //            .Where(t => t.Id == teamId)
        //            .Select(t => t.UserTeams)
        //            .FirstOrDefaultAsync();

        //        // Ensure the teamLeader is not null and return the UserName
        //        return teamLeader;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception as needed
        //        // For simplicity, returning null if any exception occurs
        //        return null;
        //    }
        //}

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
      // Attempt to find the user by email
      var user = await userManager.FindByIdAsync(userId);

      // If the user is not found by email, attempt to find by username
      if (user == null)
      {
        user = await userManager.FindByIdAsync(userId);
      }
      // Return the user's ID if found, otherwise return null
      return user.Id;
    }
  }
}