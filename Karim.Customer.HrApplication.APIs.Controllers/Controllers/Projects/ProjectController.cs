using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Projects;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Projects
{
    public class ProjectController(IServicesManager _servicesManager) : ApiBaseController
    {
        [HttpGet("GetAllProjects")]
        public async Task<ActionResult<DataWithPagination<ICollection<ProjectToReturnDto>>>> getAllProjects([FromQuery]ProjectParameters? parameters)
        {
            var result = await _servicesManager.ProjectService.GetAllProjects(parameters);
            return Ok(result);
        }

        [HttpGet("GetProjectById")]
        public async Task<ActionResult<ProjectDetailsToReturnDto>> getProjectBuId(string? ProjectId)
        {
            var result = await _servicesManager.ProjectService.GetProjectById(ProjectId);
            return Ok(result);
        }

        [HttpPost("CreateProject")]
        public async Task<ActionResult<ActionStatusDto>> createProject([FromBody]ProjectToAddDto? Project)
        {
            var result = await _servicesManager.ProjectService.CreateProject(Project);
            return Ok(result);
        }

        [HttpPut("UpdateProject")]
        public async Task<ActionResult<ActionStatusDto>> updateProject([FromBody]ProjectToUpdateDto? Project)
        {
            var result = await _servicesManager.ProjectService.UpdateProject(Project);
            return Ok(result);
        }

        [HttpPut("ActivateProject")]
        public async Task<ActionResult<ActionStatusDto>> activateProject(string? ProjectId)
        {
            var result = await _servicesManager.ProjectService.ActivateProject(ProjectId);
            return Ok(result);
        }

        [HttpPut("CancelProject")]
        public async Task<ActionResult<ActionStatusDto>> activateProject([FromBody]ProjectToCancelDto? cancelDto)
        {
            var result = await _servicesManager.ProjectService.CancelProject(cancelDto);
            return Ok(result);
        }

        [HttpPut("HoldProject")]
        public async Task<ActionResult<ActionStatusDto>> holdProject(string? ProjectId)
        {
            var result = await _servicesManager.ProjectService.HoldProject(ProjectId);
            return Ok(result);
        }

        [HttpDelete("DeleteProject")]
        public async Task<ActionResult<ActionStatusDto>> deleteProject(string? ProjectId)
        {
            var result = await _servicesManager.ProjectService.DeleteProject(ProjectId);
            return Ok(result);
        }

        [HttpGet("GenerateMaxProjectCode")]
        public async Task<ActionResult<MaxCodeResult>> generateMaxCode()
        {
            var result = await _servicesManager.ProjectService.CreateMaxProjectCode();
            return Ok(result);
        }

        [HttpPut("AssignProjectToDepartment")]
        public async Task<ActionResult<ActionStatusDto>> assignProjectToDepartment(ProjectToAssignDto? data)
        {
            var result = await _servicesManager.ProjectService.AssignProjectToDepartment(data);
            return Ok(result);
        }

        [HttpGet("GetAllCurrencies")]
        public ICollection<EnumDto> GetAllCurrencies()
        {
            var result = _servicesManager.ProjectService.GetAllCurrencies();
            return result;
        }

        [HttpGet("GetAllProjectTypes")]
        public ICollection<EnumDto> GetAllProjectTypes()
        {
            var result = _servicesManager.ProjectService.GetAllProjectsTypes();
            return result;
        }

        [HttpGet("GetAllProjectStatus")]
        public ICollection<EnumDto> GetAllProjectStatus()
        {
            var result = _servicesManager.ProjectService.GetAllProjectsStatus();
            return result;
        }

        [HttpGet("FillProjects")]
        public async Task<ActionResult<ICollection<FillEntityDto<string>>>> FillProjects()
        {
            var result = await _servicesManager.ProjectService.FillProjects();
            return Ok(result);
        }
    }
}
