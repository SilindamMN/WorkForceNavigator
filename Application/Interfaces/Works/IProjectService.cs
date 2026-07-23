namespace Application.Interfaces.Works
{
    using Application.Dtos.Work.Projects;
    using Domain.Dtos.General;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsWithClientsAsync();
        Task<GeneralServiceResponseDto> CreateProjectAsync(CreateUpdateProjectDto projectDto);
        Task<GeneralServiceResponseDto> UpdateProjectAsync(CreateUpdateProjectDto projectDto);
        Task<IEnumerable<UserProjectsDto>> GetUserProjectAsync(string username);
    }
}