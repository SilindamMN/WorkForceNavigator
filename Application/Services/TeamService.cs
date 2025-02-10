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

  public class TeamService : ITeamInterface
  {
    private readonly DataContext dataContext;
    private readonly UserManager<ApplicationUser> userManager;

    public TeamService(DataContext dataContext, UserManager<ApplicationUser> userManager)
    {
      this.dataContext = dataContext;
      this.userManager = userManager;
    }

    public async Task<GeneralServiceResponseDto> UpdateTeamMembership(string username, int? teamId = null)
    {
      try
      {
        var assignee = dataContext.Users.FirstOrDefault(x => x.UserName == username);
        var team = teamId.HasValue ? dataContext.Teams.FirstOrDefault(x => x.Id == teamId.Value) : null;

        if (assignee == null)
        {
          return ResponseHelper.CreateResponse(false, 400, "Username not found.");
        }
        // Check if the user is already a member of the specified team
        if (teamId.HasValue && await IsUserInTeam(username, teamId.Value))
        {
          return ResponseHelper.CreateResponse(false, 400, "User is already a member of the specified team.");
        }

        // Check if the user is already a member of 3 teams
        if (teamId.HasValue && await IsUserInMaxTeams(username))
        {
          return ResponseHelper.CreateResponse(false, 400, "User is already a member of the maximum allowed 3 teams.");
        }

        // If teamId is provided, update it; otherwise, remove the member from the team
        if (teamId.HasValue)
        {
          // Add the user to the team
          var userTeam = new UserTeam
          {
            UserId = assignee.Id,
            TeamId = teamId.Value
          };
          dataContext.UserTeams.Add(userTeam);
          await dataContext.SaveChangesAsync();
        }
        else
        {
          // Remove the user from all teams
          var userTeams = dataContext.UserTeams.Where(ut => ut.UserId == assignee.Id);
          dataContext.UserTeams.RemoveRange(userTeams);
        }

        // Assign the line manager
        if (assignee.LineManager == null)
        {
          var lineManager = teamId.HasValue ? await AssignLineManager(teamId.Value) : null;
          assignee.LineManager = lineManager;
        }
        await dataContext.SaveChangesAsync();

        // Determine the action based on teamId: added or removed
        var action = teamId.HasValue ? "added" : "removed";

        // Return response with appropriate message
        return ResponseHelper.CreateResponse(true, 200, $"Member {action} successfully.");
      }
      catch (Exception ex)
      {
        return ResponseHelper.CreateResponse(false, 500, $"An error occurred: {ex.Message}");
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
                                        TeamLeader = t.TeamLeader,
                                        Member = new MemberDetails
                                        {
                                          FirstName = u.FirstName,
                                          LastName = u.LastName,
                                          JobTitle = jt.Title
                                        }
                                      }).ToListAsync();

        var teams = teamsWithMembers.GroupBy(t => new { t.TeamName, t.TeamLeader })
                                    .Select(g => new TeamMemberDetailsDto
                                    {
                                      TeamLeader = g.Key.TeamLeader,
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
          TeamLeader = teamDto.TeamLeader,
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

        // Create a new UserTeam record linking the team leader to the new team
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

    // Private method to assign Line Manager when adding a new member to a team
    private async Task<string> AssignLineManager(int teamId)
    {
      try
      {
        // Retrieve the TeamLeader's username for the specified team
        var teamLeaderUsername = dataContext.Teams
            .Where(t => t.Id == teamId)
            .Select(t => t.TeamLeader)
            .FirstOrDefault();

        return teamLeaderUsername;
      }
      catch (Exception ex)
      {
        // Log or handle the exception as needed
        // For simplicity, returning null if any exception occurs
        return null;
      }
    }

    // Private method to check if the user is already a member of the maximum allowed teams
    private async Task<bool> IsUserInMaxTeams(string username)
    {
      // Find the user by username
      var user = await dataContext.Users.SingleOrDefaultAsync(u => u.UserName == username);

      if (user == null)
      {
        throw new InvalidOperationException("User not found.");
      }

      // Count the number of distinct teams the user is a member of
      var teamMembershipCount = await dataContext.UserTeams
          .Where(ut => ut.UserId == user.Id)
          .Select(ut => ut.TeamId)
          .Distinct()
          .CountAsync();

      // Returns true if the user is a member of 3 or more teams
      return teamMembershipCount >= 3;
    }

    // Private method to check if the user is already a team leader in any team
    private async Task<bool> IsUserTeamLeaderInAnyTeam(string username)
    {
      // Count the number of teams the user is a team leader of
      var teamLeaderCount = await dataContext.Teams.CountAsync(t => t.TeamLeader == username);
      return teamLeaderCount > 0;
    }

    private async Task<bool> IsUserInTeam(string username, int teamId)
    {
      return await dataContext.UserTeams.AnyAsync(u => u.UserId == username && u.TeamId == teamId);
    }

    private async Task<string> GetUserIdAsync(string userName)
    {
      // Attempt to find the user by email
      var user = await userManager.FindByNameAsync(userName);

      // If the user is not found by email, attempt to find by username
      if (user == null)
      {
        user = await userManager.FindByNameAsync(userName);
      }
      // Return the user's ID if found, otherwise return null
      return user.Id;
    }
  }
}