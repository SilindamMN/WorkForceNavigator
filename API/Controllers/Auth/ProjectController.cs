
namespace API.Controllers.Auth
{
    using Application.Dtos.Work.Projects;
    using Application.Interfaces;
    using Application.Interfaces.Works;
    using Application.Services;
    using Application.Services.Auth;
    using Domain.Enties.Work;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IGenericService<Project, CreateUpdateProjectDto> genericService;

        public ProjectController(IProjectService projectService, IGenericService<Project, CreateUpdateProjectDto> genericService)
        {
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

        [HttpPost("create")]
        [ProducesResponseType(typeof(Project), 201)] // Define the response type for successful creation
        public async Task<IActionResult> CreateNewProject([FromBody] CreateUpdateProjectDto projectDto)
        {
            var result = await projectService.CreateProjectAsync(projectDto);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPatch]
        [Route("update")]
        [ProducesResponseType(typeof(Project), 200)] // Define the response type for successful update
        [ProducesResponseType(404)] // Define the response type for not found
        public async Task<IActionResult> UpdateProject(int id, [FromBody] CreateUpdateProjectDto projectDto)
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
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<UserProjectsDto>>> GetUserProject(string username)
        {
            try
            {
                var projects = await projectService.GetUserProjectAsync(username);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving projects: {ex.Message}");
            }
        }
    }
}