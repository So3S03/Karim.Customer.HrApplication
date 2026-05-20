using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Task
{
    public class TaskController(IServicesManager _servicesManager): ApiBaseController
    {
        [HttpGet("GetAllTasks")]
        public async Task<ActionResult<DataWithPagination<ICollection<TaskToReturnDto>>>> GetAllTasks([FromQuery]TaskParameters parameters)
        {
            var tasks = await _servicesManager.TaskService.GetAllTasks(parameters);
            return Ok(tasks);
        }

        [HttpGet("GetSpecificTask")]
        public async Task<ActionResult<TaskDetailsToReturnDto>> GetSpecificTask(string Id)
        {
            var tasks = await _servicesManager.TaskService.GetTaskById(Id);
            return Ok(tasks);
        }

        [HttpGet("GenerateMaxCode")]
        public async Task<ActionResult<MaxCodeResult>> GenerateMaxCode()
        {
            var tasks = await _servicesManager.TaskService.GenerateMaxCode();
            return Ok(tasks);
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult<ActionStatusDto>> AddTask([FromBody]TaskToAddDto? task)
        {
            var tasks = await _servicesManager.TaskService.AddTask(task);
            return Ok(tasks);
        }

        [HttpPut("UpdateTask")]
        public async Task<ActionResult<ActionStatusDto>> UpdateTask([FromBody]TaskToUpdateDto? task)
        {
            var tasks = await _servicesManager.TaskService.UpdateTask(task);
            return Ok(tasks);
        }

        [HttpPut("PullTask")]
        public async Task<ActionResult<ActionStatusDto>> PullTask([FromBody]TaskToPullDto? task)
        {
            var tasks = await _servicesManager.TaskService.PullingTask(task);
            return Ok(tasks);
        }

        [HttpDelete("DeleteTask")]
        public async Task<ActionResult<ActionStatusDto>> DeleteTask(string Id)
        {
            var tasks = await _servicesManager.TaskService.DeleteTask(Id);
            return Ok(tasks);
        }

        [HttpPut("ArchiveTask")]
        public async Task<ActionResult<ActionStatusDto>> ArchiveTask(string Id)
        {
            var tasks = await _servicesManager.TaskService.ArchiveTask(Id);
            return Ok(tasks);
        }

        [HttpPut("CloseTask")]
        public async Task<ActionResult<ActionStatusDto>> CloseTask(string Id)
        {
            var tasks = await _servicesManager.TaskService.CloseTask(Id);
            return Ok(tasks);
        }

        [HttpPut("ReOpenTask")]
        public async Task<ActionResult<ActionStatusDto>> ReOpenTask(string Id)
        {
            var tasks = await _servicesManager.TaskService.ReOpenTask(Id);
            return Ok(tasks);
        }

        [HttpPut("UnArchiveTask")]
        public async Task<ActionResult<ActionStatusDto>> UnArchiveTask(string Id)
        {
            var tasks = await _servicesManager.TaskService.UnArchiveTask(Id);
            return Ok(tasks);
        }
    }
}
