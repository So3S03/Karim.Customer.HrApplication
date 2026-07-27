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
    }
}
