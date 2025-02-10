namespace Application.Services.GenericServices
{
  using Application.Helpers;
  using Application.Interfaces;
  using AutoMapper;
  using Domain.Dtos.General;
  using Domain.Dtos.GeneralAdmin;
  using Domain.Enties;
  using Microsoft.EntityFrameworkCore;
  using Persistence;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class ProjectService : IProjectService
  {
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public ProjectService(DataContext dataContext, IMapper mapper)
    {
      this.dataContext = dataContext;
      this.mapper = mapper;
    }
    public async Task<GeneralServiceResponseDto> CreateProjectAsync(CreateProjectDto projectDto)
    {
      try
      {
        // Check if ProjectName exists
        if (await ProjectExistsAsync(projectDto.ProjectName))
        {
          return ResponseHelper.CreateResponse(false, 400, $"Project Name{projectDto.ProjectName} does  exist.");
        }
        // Check if ClientId exists
        if (!await ClientExistsAsync(projectDto.ClientId))
        {
          return ResponseHelper.CreateResponse(false, 400, $"Client with ID {projectDto.ClientId} does not exist.");
        }

        // Check if TeamId exists
        if (!await TeamExistsAsync(projectDto.TeamId))
        {
          return ResponseHelper.CreateResponse(false, 400, $"Team with ID {projectDto.TeamId} does not exist.");
        }
        // Check if EndDate is after StartDate
        if (!IsEndDateAfterStartDate(projectDto.StartDate, projectDto.EndDate))
        {
          return ResponseHelper.CreateResponse(false, 400, "End Date must be after Start Date.");
        }
        var project = new CreateProjectDto
        {
          ProjectName = projectDto.ProjectName,
          ClientId = projectDto.ClientId,
          Description = projectDto.Description,
          StartDate = projectDto.StartDate,
          EndDate = projectDto.EndDate,
          TeamId = projectDto.TeamId
        };
        var projectMap = mapper.Map<Project>(projectDto);
        dataContext.Projects.Add(projectMap);
        await dataContext.SaveChangesAsync();
        return ResponseHelper.CreateResponse(true, 200, "Project Created Successfully");
      }
      catch (Exception ex)
      {
        // Handle exceptions or rethrow as needed
        throw new Exception("Failed to create project.", ex);
      }
    }

    public async Task<GeneralServiceResponseDto> UpdateProjectAsync(int projectId, CreateProjectDto projectDto)
    {
      try
      {
        // Check if the project exists
        var existingProject = await dataContext.Projects.FindAsync(projectId);
        if (existingProject == null)
        {
          return ResponseHelper.CreateResponse(false, 404, $"Project with ID {projectId} does not exist.");
        }

        // Check if ClientId exists
        if (!await ClientExistsAsync(projectDto.ClientId))
        {
          return ResponseHelper.CreateResponse(false, 400, $"Client with ID {projectDto.ClientId} does not exist.");
        }

        // Check if TeamId exists
        if (!await TeamExistsAsync(projectDto.TeamId))
        {
          return ResponseHelper.CreateResponse(false, 400, $"Team with ID {projectDto.TeamId} does not exist.");
        }

        // Check if EndDate is after StartDate
        if (!IsEndDateAfterStartDate(projectDto.StartDate, projectDto.EndDate))
        {
          return ResponseHelper.CreateResponse(false, 400, "End Date must be after Start Date.");
        }

        // Update the project properties
        existingProject.ProjectName = projectDto.ProjectName;
        existingProject.ClientId = projectDto.ClientId;
        existingProject.Description = projectDto.Description;
        existingProject.StartDate = projectDto.StartDate;
        existingProject.EndDate = projectDto.EndDate;
        existingProject.TeamId = projectDto.TeamId;

        // Save changes to the database
        dataContext.Projects.Update(existingProject);
        await dataContext.SaveChangesAsync();

        return ResponseHelper.CreateResponse(true, 200, "Project Updated Successfully");
      }
      catch (Exception ex)
      {
        // Handle exceptions or rethrow as needed
        throw new Exception("Failed to update project.", ex);
      }
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsWithClientsAsync()
    {
      var projects = (from project in dataContext.Projects
                      join client in dataContext.Clients on project.ClientId equals client.Id
                      join team in dataContext.Teams on project.TeamId equals team.Id
                      select new ProjectDto
                      {
                        ProjectName = project.ProjectName,
                        Description = project.Description,
                        StartDate = project.StartDate,
                        EndDate = project.EndDate,
                        ClientName = client.ClientName,
                        TeamName = team.TeamName
                      }).ToList();
      return projects;
    }
    private Task<bool> ClientExistsAsync(int clientId)
    {
      return dataContext.Clients.AnyAsync(c => c.Id == clientId);
    }

    private Task<bool> TeamExistsAsync(int teamId)
    {
      return dataContext.Teams.AnyAsync(t => t.Id == teamId);
    }

    private Task<bool> ProjectExistsAsync(string projectName)
    {
      return dataContext.Projects.AnyAsync(t => t.ProjectName == projectName);
    }

    private bool IsEndDateAfterStartDate(DateTime startDate, DateTime endDate)
    {
      return endDate > startDate;
    }

    public Task<GeneralServiceResponseDto> UpdateProjectAsync(CreateProjectDto projectDto)
    {
      throw new NotImplementedException();
    }
  }
}