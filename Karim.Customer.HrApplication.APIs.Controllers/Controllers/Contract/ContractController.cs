using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Contract
{
    public class ContractController(IServicesManager _servicesManager) : ApiBaseController
    {
        [HttpGet("GetContractMaxCode")]
        public async Task<ActionResult<MaxCodeResult>> GetContractMaxCode()
        {
            var result = await _servicesManager.ContractService.GetContractCode();
            return Ok(result);
        }

        [HttpPost("AddEmployeeContract")]
        public async Task<ActionResult<ActionStatusDto>> AddEmployeeContract([FromBody] EmployeeContractToAddDto? data)
        {
            var result = await _servicesManager.ContractService.AddEmployeeContract(data);
            return Ok(result);
        }
    }
}
