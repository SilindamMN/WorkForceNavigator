
namespace API.Controllers.Auth
{
  using Application.Interfaces;

  using Application.Interfaces.GenericInterfaces;
  using Application.Services;
  using Application.Services.Auth;
  using Application.Services.GenericServices;
  using Domain.Dtos.Account;
  using Domain.Dtos.GeneralAdmin;
  using Domain.Enties;
  using Domain.Entities.TimeSheets;
  using Microsoft.AspNetCore.Mvc;

  [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
    private readonly IProjectService projectService;
    private readonly IGenericService<Project, CreateProjectDto> genericService;

    public ProjectController(IProjectService projectService,IGenericService<Project,CreateProjectDto> genericService)
        {
      this.projectService = projectService;
      this.projectService = projectService;
      this.genericService = genericService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
      try
      {
        var projects = await projectService.GetAllProjectsWithClientsAsync();
        return Ok(projects);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occurred while retrieving projects: {ex.Message}");
      }
    }

    [HttpPost("CreateProject")]
    [ProducesResponseType(typeof(Project), 201)] // Define the response type for successful creation
    [ProducesResponseType(typeof(IDictionary<string, string[]>), 400)] // Define the response type for validation errors
    public async Task<IActionResult> CreateNewProject([FromBody] CreateProjectDto projectDto)
    {
      var result = await projectService.CreateProjectAsync(projectDto);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }

    [HttpPost]
    [Route("UpdateProject")]
    [ProducesResponseType(typeof(Project), 200)] // Define the response type for successful update
    [ProducesResponseType(typeof(IDictionary<string, string[]>), 400)] // Define the response type for validation errors
    [ProducesResponseType(404)] // Define the response type for not found
    public async Task<IActionResult> UpdateProject(int id, [FromBody] CreateProjectDto projectDto)
    {
      try
      {
        var updatedProject = await genericService.UpdateAsync(id, projectDto);
        if (updatedProject == null)
        {
          return NotFound("Project not found");
        }

        return Ok(updatedProject);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occurred while updating project: {ex.Message}");
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
      var result = await genericService.GetByIdAsync(id);
      if (result is null)
      {
        return NotFound("Project not found");
      }
      return Ok(result);
    }
  }
}