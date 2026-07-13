using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Tasks;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Task
{
    public interface ITaskService
    {
        Task<MaxCodeResult> GenerateMaxCode(); 
        Task<ActionStatusDto> AddTask(TaskToAddDto? task);
        Task<ActionStatusDto> UpdateTask(TaskToUpdateDto? task);
        Task<TaskDetailsToReturnDto> GetTaskById(string? Id);
        Task<ActionStatusDto> CloseTask(string? Id);
        Task<ActionStatusDto> ReOpenTask(string? Id);
        Task<ActionStatusDto> ArchiveTask(string? Id);
        Task<ActionStatusDto> UnArchiveTask(string? Id);
        Task<ActionStatusDto> PullingTask(TaskToPullDto? data);
        Task<DataWithPagination<ICollection<TaskToReturnDto>>> GetAllTasks(TaskParameters parameters);
        Task<ActionStatusDto> DeleteTask(string? Id);
        ICollection<EnumDto> GetTaskToPullStatuses();
    }
}
