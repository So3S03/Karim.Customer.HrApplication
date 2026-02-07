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
    }
}
