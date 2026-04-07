using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Projects;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Projects
{
    public interface IProjectServices
    {
        Task<DataWithPagination<ICollection<ProjectToReturnDto>>> GetAllProjects(ProjectParameters? parameters);
        Task<ProjectDetailsToReturnDto> GetProjectById(string? Id);
        Task<ActionStatusDto> CreateProject(ProjectToAddDto? project);
        Task<ActionStatusDto> UpdateProject(ProjectToUpdateDto? project);
        Task<ActionStatusDto> DeleteProject(string? Id);

    }
}
