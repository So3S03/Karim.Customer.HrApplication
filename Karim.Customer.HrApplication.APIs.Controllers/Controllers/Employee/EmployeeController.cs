using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Employee
{
    public class EmployeeController(IServicesManager _serviecManager): ApiBaseController
    {
        [HttpGet("GetMaxEmployeeCode")]
        public async Task<ActionResult<MaxCodeResult>> GetMaxEmployeeCode()
        {
            var result = await _serviecManager.EmployeeService.GenerateEmployeeMaxCodeAsync();
            return Ok(result);
        }

        [HttpGet("GetEmployeeSortingLockup")]
        public ActionResult<ICollection<EnumDto>> EmployeeSortingLockup()
        {
            var result = _serviecManager.EmployeeService.EmployeeSortingLockup();
            return Ok(result);
        }

        [HttpGet("GetContractExistLockup")]
        public ActionResult<ICollection<EnumDto>> GetContractExistLockup()
        {
            var result = _serviecManager.EmployeeService.GetContractExistLockup();
            return Ok(result);
        }

        [HttpGet("GetEmployeeStatusLockup")]
        public ActionResult<ICollection<EnumDto>> GetEmployeeStatusLockup()
        {
            var result = _serviecManager.EmployeeService.GetEmployeeStatusLockup();
            return Ok(result);
        }

        [HttpGet("GetEmployeeTypeLockup")]
        public ActionResult<ICollection<EnumDto>> GetEmployeeTypeLockup()
        {
            var result = _serviecManager.EmployeeService.GetEmployeeTypeLockup();
            return Ok(result);
        }

        [HttpGet("GetEmployeeWorkTypeLockup")]
        public ActionResult<ICollection<EnumDto>> GetEmployeeWorkTypeLockup()
        {
            var result = _serviecManager.EmployeeService.GetEmployeeWorkTypeLockup();
            return Ok(result);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<DataWithPagination<ICollection<EmployeeToReturnDto>>>> GetAllEmployees([FromQuery]EmployeeQueryParameters? parameters)
        {
            var result = await _serviecManager.EmployeeService.GetAllEmployeeWithPaginationAsync(parameters);
            return Ok(result);
        }

        [HttpGet("GetSpecificEmployeeById")]
        public async Task<ActionResult<SpecificEmployeeToReturnDto>> GetSpecificEmployeeById(string? Id)
        {
            var result = await _serviecManager.EmployeeService.GetSpecificEmployeeByIdAsync(Id);
            return Ok(result);
        }

        [HttpGet("GetEmployeeRankLockup")]
        public ActionResult<ICollection<EnumDto>> GetEmployeeRankLockup()
        {
            var result = _serviecManager.EmployeeService.GetEmployeeRankLockup();
            return Ok(result);
        }

        [HttpPost("AddNewEmployee")]
        public async Task<ActionResult<ActionStatusDto>> AddNewEmployee([FromForm]SingleEmployeeToAddDto? Emp, IFormFile? Photo)
        {
            var result = await _serviecManager.EmployeeService.AddNewEmployeeAsync(Emp, Photo);
            return Ok(result);
        }

        [HttpGet("FillDepartments")]
        public async Task<ActionResult<ICollection<FillEntityDto<string>>>> FillDepartments(string? Name)
        {
            var result = await _serviecManager.EmployeeService.FillDepartmentsAsync(Name);
            return Ok(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult<ActionStatusDto>> UpdateEmployee([FromForm]SingleEmployeeToUpdateDto? entity, IFormFile? Photo)
        {
            var result = await _serviecManager.EmployeeService.UpdateEmployeeAsync(entity, Photo);
            return Ok(result);
        }

        [HttpDelete("RemoveEmployeeTemporarly")]
        public async Task<ActionResult<ActionStatusDto>> RemoveEmployeeTemporarly(string? Id)
        {
            var result = await _serviecManager.EmployeeService.RemoveEmployeeTemporarly(Id);
            return Ok(result);
        }

        [HttpDelete("RemoveEmployeePermenetly")]
        public async Task<ActionResult<ActionStatusDto>> RemoveEmployeePermenetly(string? Id)
        {
            var result = await _serviecManager.EmployeeService.RemoveEmployeePermenetly(Id);
            return Ok(result);
        }

        [HttpPut("RestoreRemovedEmployee")]
        public async Task<ActionResult<ActionStatusDto>> RestoreRemovedEmployee(string? Id)
        {
            var result = await _serviecManager.EmployeeService.RestoreRemovedEmployee(Id);
            return Ok(result);
        }

        [HttpPut("UploadEmployeePhoto")]
        public async Task<ActionResult<ActionStatusDto>> UploadEmployeePhoto([FromForm]string? EmpId, IFormFile? File)
        {
            var result = await _serviecManager.EmployeeService.UploadEmployeePhoto(EmpId, File);
            return Ok(result);
        }

        [HttpDelete("DeleteEmployeePhoto")]
        public async Task<ActionResult<ActionStatusDto>> DeleteEmployeePhoto(string? EmpId)
        {
            var result = await _serviecManager.EmployeeService.DeleteEmployeePhoto(EmpId);
            return Ok(result);
        }

        [HttpDelete("TerminateEmployee")]
        public async Task<ActionResult<ActionStatusDto>> TerminateEmployee(string? EmpId, bool isRequestDeleteEmp)
        {
            var result = await _serviecManager.EmployeeService.TerminateEmployee(EmpId, isRequestDeleteEmp);
            return Ok(result);
        }

        [HttpPut("UndoTerminatedEmployee")]
        public async Task<ActionResult<ActionStatusDto>> UndoTerminatedEmployee(string? EmpId)
        {
            var result = await _serviecManager.EmployeeService.UndoTerminatedEmployee(EmpId);
            return Ok(result);
        }

        [HttpPut("TerminateCollectiveEmployees")]
        public async Task<ActionResult<ActionStatusDto>> TerminateCollectiveEmployees(List<string>? Ids)
        {
            var resullt = await _serviecManager.EmployeeService.TerminateCollectiveEmployees(Ids);
            return Ok(resullt);
        }
    }
}
