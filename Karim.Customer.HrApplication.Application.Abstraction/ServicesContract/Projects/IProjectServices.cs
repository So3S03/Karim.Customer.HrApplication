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
        Task<ActionStatusDto> ActivateProject(string? Id);
        Task<ActionStatusDto> CancelProject(ProjectToCancelDto? cancelDto);
        Task<ActionStatusDto> HoldProject(string? Id);
        Task<MaxCodeResult> CreateMaxProjectCode();
        Task<ActionStatusDto> AssignProjectToDepartment(ProjectToAssignDto? data);
        ICollection<EnumDto> GetAllCurrencies();
        ICollection<EnumDto> GetAllProjectsTypes();
        ICollection<EnumDto> GetAllProjectsStatus();
        Task<ICollection<FillEntityDto<string>>> FillProjects();
    }
}
