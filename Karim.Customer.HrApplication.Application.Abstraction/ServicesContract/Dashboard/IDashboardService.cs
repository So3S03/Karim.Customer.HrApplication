using Karim.Customer.HrApplication.Shared.DTOs.Dashboard;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Dashboard
{
    public interface IDashboardService
    {
        Task<CompanyStatusToReturnDto> GetCompanyStatusDto();
    }
}
