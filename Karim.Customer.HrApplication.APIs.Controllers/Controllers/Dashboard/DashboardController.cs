using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Dashboard
{
    public class DashboardController(IServicesManager _servicesManager) : ApiBaseController
    {
        [HttpGet("GetCompanyStatus")]
        public async Task<ActionResult<CompanyStatusToReturnDto>> GetCompanyStatus()
        {
            var result = await _servicesManager.DashboardService.GetCompanyStatusDto();
            return Ok(result);
        }
        [HttpGet("GetYearlyPayrollsSumComparisons")]
        public async Task<ActionResult<ICollection<PayrollComparisonPerMonthDto>>> GetYearlyPayrollsSumComparisons(int? year)
        {
            var result = await _servicesManager.DashboardService.GetMonthlyPayrollsSumComparison(year);
            return Ok(result);
        }
        [HttpGet("GetYearlyAttendancesRateComparisons")]
        public async Task<ActionResult<ICollection<AllEmployeesAttendanceRatePerMonthDto>>> GetYearlyAttendancesRateComparisons(int? year)
        {
            var result = await _servicesManager.DashboardService.GetAttendanceRatePerMonthComparison(year);
            return Ok(result);
        }
        [HttpGet("GetYearlyHiringVsResignedOrTerminatedEmployees")]
        public async Task<ActionResult<ICollection<HiringVsResignedOrTerminatedEmployeesDto>>> GetYearlyHiringVsResignedOrTerminatedEmployees(int? year)
        {
            var result = await _servicesManager.DashboardService.GetHiringVsResignedOrTermiunatedPerMonthComparison(year);
            return Ok(result);
        }
        [HttpGet("GetCountOfEmployeesPerDepartment")]
        public async Task<ActionResult<ICollection<CountOfEmployeeInDepartmentsDto>>> GetCountOfEmployeesPerDepartment()
        {
            var result = await _servicesManager.DashboardService.GetCountOfEmployeesInDepartments();
            return Ok(result);
        }
        [HttpGet("GetCountOfEmployeesPerType")]
        public async Task<ActionResult<ICollection<EmployeesTypesCountDto>>> GetCountOfEmployeesPerType()
        {
            var result = await _servicesManager.DashboardService.GetCountOfEmployeesPerTypes();
            return Ok(result);
        }
    }
}
