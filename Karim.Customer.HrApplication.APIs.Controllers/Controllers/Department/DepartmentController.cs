using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Department
{
    public class DepartmentController(IServicesManager servicesManager) : ApiBaseController
    {
        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult<ICollection<DepartmentToReturnDto>>> GetAllDepartments(int? type, int? status = 0)
        {
            var result = await servicesManager.DepartmentService.GetDepartmentsAsync(status, type);
            return Ok(result);
        }

        [HttpGet("FillDepartmentStatusLockUp")]
        public ActionResult<ICollection<EnumDto>> FillDepartmentStatusLockUp()
        {
            var result = servicesManager.DepartmentService.FillDepartmentsStatus();
            return Ok(result);
        }

        [HttpGet("FillDepartmentTypesLockUp")]
        public ActionResult<ICollection<EnumDto>> FillDepartmentTypesLockUp()
        {
            var result = servicesManager.DepartmentService.FillDepartmentTypes();
            return Ok(result);
        }

        [HttpGet("GetDepartmentById")]
        public async Task<ActionResult<DepartmentToReturnDto>> GetDepartmentById(string? Id)
        {
            var result = await servicesManager.DepartmentService.GetDepartmentByIdAsync(Id);
            return Ok(result);
        }

        [HttpPut("DepartmentActiveToggle")]
        public async Task<ActionResult<ActionStatusDto>> ActivationToggleForDepartment(string? id, bool? status)
        {
            var Result = await servicesManager.DepartmentService.DepartmentActiveToggle(id, status);
            return Ok(Result);
        }

        [HttpPut("SoftRemoveDepartment")]
        public async Task<ActionResult<ActionStatusDto>> SoftDeleteDepartment(string? id)
        {
            var Result = await servicesManager.DepartmentService.SoftRemoveDepartment(id);
            return Ok(Result);
        }

        [HttpPut("RestoreDepartment")]
        public async Task<ActionResult<ActionStatusDto>> ActivationToggleForDepartment(string? id)
        {
            var Result = await servicesManager.DepartmentService.RestoreRemovedDepartment(id);
            return Ok(Result);
        }

        [HttpPost("AddDepartment")]
        public async Task<ActionResult<ActionStatusDto>> AddDepartment(DepartmentToAddDto? entity)
        {
            var result = await servicesManager.DepartmentService.AddDepartmentAsync(entity);
            return Ok(result);
        }

        [HttpPut("UpdateDepartment")]
        public async Task<ActionResult<ActionStatusDto>> UpdateDepartment(DepartmentToUpdateDto? entity)
        {
            var result = await servicesManager.DepartmentService.UpdateDepartment(entity);
            return Ok(result);
        }

        [HttpDelete("DeleteDepartment")]
        public async Task<ActionResult<ActionStatusDto>> DeleteDepartment(string? id)
        {
            var result = await servicesManager.DepartmentService.DeleteDepartment(id);
            return Ok(result);
        }

    }
}
