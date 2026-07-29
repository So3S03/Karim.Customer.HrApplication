using Karim.Customer.HrApplication.Shared.DTOs.Dashboard;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Dashboard
{
    public interface IDashboardService
    {
        Task<CompanyStatusToReturnDto> GetCompanyStatusDto();
        Task<ICollection<PayrollComparisonPerMonthDto>> GetMonthlyPayrollsSumComparison(int? year);
        Task<ICollection<AllEmployeesAttendanceRatePerMonthDto>> GetAttendanceRatePerMonthComparison(int? year);
        Task<ICollection<HiringVsResignedOrTerminatedEmployeesDto>> GetHiringVsResignedOrTermiunatedPerMonthComparison(int? year);
        Task<ICollection<CountOfEmployeeInDepartmentsDto>> GetCountOfEmployeesInDepartments();
    }
}
