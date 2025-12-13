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
            var result = await servicesManager.DepartmentService.GetDepartments(status, type);
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
            var result = await servicesManager.DepartmentService.GetDepartmentById(Id);
            return Ok(result);
        }
        [HttpPost("AddDepartment")]
        public async Task<ActionResult<ActionStatusDto<DepartmentToReturnDto>>> AddDepartment(DepartmentToAddDto entity)
        {
            var result = await servicesManager.DepartmentService.AddDepartment(entity);
            return Ok(result);
        }

    }
}
