using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Payroll
{
    public class PayrollController(IServicesManager _servicesManager) : ApiBaseController
    {
        [HttpGet("GetAllPayslipsPerMonth")]
        public async Task<ActionResult<DataWithPagination<ICollection<PayslipToReturnDto>>>> GetAllPayslipsPerMonth([FromQuery]PayrollParameter parameter)
        {
            var result = await _servicesManager.PayrollService.GetAllEmployeesPayslipsPerMonth(parameter);
            return Ok(result);
        }

        [HttpGet("GetAllEmployeePayslips")]
        public async Task<ActionResult<DataWithPagination<ICollection<PayslipToReturnDto>>>> GetAllEmployeePayslips([FromQuery]EmployeePayslipsParameter parameter)
        {
            var result = await _servicesManager.PayrollService.GetEmployeeAllPayslips(parameter);
            return Ok(result);
        }

        [HttpGet("GetPayslipDetails")]
        public async Task<ActionResult<PayslipDetailsToReturnDto>> GetPayslipDetails(string? PayslipId)
        {
            var result = await _servicesManager.PayrollService.GetPayslipDetails(PayslipId);
            return Ok(result);
        }

        [HttpPut("ApproveSalary")]
        public async Task<ActionResult<ActionStatusDto>> ApproveSalary(string? PayslipId)
        {
            var result = await _servicesManager.PayrollService.ApproveSalary(PayslipId);
            return Ok(result);
        }

        [HttpPut("RePendingApprovedSalary")]
        public async Task<ActionResult<ActionStatusDto>> RePendingApprovedSalary(string? PayslipId)
        {
            var result = await _servicesManager.PayrollService.RePendingApprovedSalary(PayslipId);
            return Ok(result);
        }

        [HttpPut("PaySalary")]
        public async Task<ActionResult<ActionStatusDto>> PaySalary(PayrollToPayDto? payrollToPayDto)
        {
            var result = await _servicesManager.PayrollService.PaySalary(payrollToPayDto);
            return Ok(result);
        }

        [HttpPost("AddPenalty")]
        public async Task<ActionResult<ActionStatusDto>> AddPenalty(PenaltyToAddDto? penaltyToAddDto)
        {
            var result = await _servicesManager.PayrollService.AddPenalty(penaltyToAddDto);
            return Ok(result);
        }

        [HttpPut("EditPenalty")]
        public async Task<ActionResult<ActionStatusDto>> EditPenalty(PenaltyToEditDto? penaltyToEditDto)
        {
            var result = await _servicesManager.PayrollService.EditPenalty(penaltyToEditDto);
            return Ok(result);
        }

        [HttpDelete("DeletePenalty")]
        public async Task<ActionResult<ActionStatusDto>> DeletePenalty(string? penaltyId)
        {
            var result = await _servicesManager.PayrollService.DeletePenalty(penaltyId);
            return Ok(result);
        }

        [HttpPost("AddBonus")]
        public async Task<ActionResult<ActionStatusDto>> AddBonus(BonusToAddDto? bonusToAddDto)
        {
            var result = await _servicesManager.PayrollService.AddBonus(bonusToAddDto);
            return Ok(result);
        }

        [HttpPut("EditBonus")]
        public async Task<ActionResult<ActionStatusDto>> EditBonus(BonusToEditDto? bonusToEditDto)
        {
            var result = await _servicesManager.PayrollService.EditBonus(bonusToEditDto);
            return Ok(result);
        }

        [HttpDelete("DeleteBonus")]
        public async Task<ActionResult<ActionStatusDto>> DeleteBonus(string? bonusId)
        {
            var result = await _servicesManager.PayrollService.DeleteBonus(bonusId);
            return Ok(result);
        }

        [HttpDelete("DeleteAllowances")]
        public async Task<ActionResult<ActionStatusDto>> DeleteAllowances(string? allowanceId)
        {
            var result = await _servicesManager.PayrollService.DeleteAllowance(allowanceId);
            return Ok(result);
        }

        [HttpDelete("DeleteSalary")]
        public async Task<ActionResult<ActionStatusDto>> DeleteSalary(string? PayslipId)
        {
            var result = await _servicesManager.PayrollService.DeleteSalary(PayslipId);
            return Ok(result);
        }

        [HttpGet("GetPayslipBonusesGrid")]
        public async Task<ActionResult<DataWithPagination<ICollection<PayrollBonusToReturnDto>>>> GetPayslipBonusesGrid(PayrollRelationsParameter parameter)
        {
            var result = await _servicesManager.PayrollService.PayslipBonusesGrid(parameter);
            return Ok(result);
        }

        [HttpGet("GetPayslipPenaltiesGrid")]
        public async Task<ActionResult<DataWithPagination<ICollection<PayrollPenaltyToReturnDto>>>> GetPayslipPenaltiesGrid(PayrollRelationsParameter parameter)
        {
            var result = await _servicesManager.PayrollService.PayslipPenaltiesGrid(parameter);
            return Ok(result);
        }

        [HttpGet("GetPayslipAllowancesGrid")]
        public async Task<ActionResult<DataWithPagination<ICollection<PayrollAllowanceToReturnDto>>>> GetPayslipAllowancesGrid(PayrollRelationsParameter parameter)
        {
            var result = await _servicesManager.PayrollService.PayslipAllowancesGrid(parameter);
            return Ok(result);
        }

        [HttpPut("EditPayslip")]
        public async Task<ActionResult<ActionStatusDto>> EditPayslip(PayslipToEditDto? payslipToEditDto)
        {
            var result = await _servicesManager.PayrollService.EditEmployeePayslip(payslipToEditDto);
            return Ok(result);
        }

        [HttpPost("CreateManualPayslip")]
        public async Task<ActionResult<ActionStatusDto>> CreateManualPayslip(PayslipToAddDto? payslipToAddDto)
        {
            var result = await _servicesManager.PayrollService.CreateManualPayslip(payslipToAddDto);
            return Ok(result);
        }
    }
}
