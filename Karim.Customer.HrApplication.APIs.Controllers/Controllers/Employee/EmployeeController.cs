using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Employee
{
    public class EmployeeController(IServicesManager _serviecManager): ApiBaseController
    {
        [HttpGet("GetMaxEmployeeCode")]
        public async Task<ActionResult<MaxCodeResult>> GetMaxEmployeeCode()
        {
            var result = await _serviecManager.EmployeeService.GenerateEmployeeMaxCode();
            return Ok(result);
        }

        [HttpGet("EmployeeSortingLockup")]
        public ActionResult<ICollection<EnumDto>> EmployeeSortingLockup()
        {
            var result = _serviecManager.EmployeeService.EmployeeSortingLockup();
            return Ok(result);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<DataWithPagination<ICollection<EmployeeToReturnDto>>>> GetAllEmployees([FromQuery]EmployeeQueryParameters? parameters)
        {
            var result = await _serviecManager.EmployeeService.GetAllEmployeeWithPagination(parameters);
            return Ok(result);
        }
    }
}
