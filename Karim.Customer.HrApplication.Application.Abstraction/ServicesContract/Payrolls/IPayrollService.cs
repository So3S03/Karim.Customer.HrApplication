using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Payrolls
{
    public interface IPayrollService
    {
        Task<DataWithPagination<ICollection<PayslipToReturnDto>>> GetAllEmployeesPayslipsPerMonth(PayrollParameter parameter);
        Task<DataWithPagination<ICollection<PayslipToReturnDto>>> GetEmployeeAllPayslips(EmployeePayslipsParameter parameter);
        Task<ActionStatusDto> ApproveSalary(string? PayslipId);
        Task<ActionStatusDto> PaySalary(PayrollToPayDto? payrollToPayDto);
        Task<PayslipDetailsToReturnDto> GetPayslipDetails(string? PayslipId);
        Task<ActionStatusDto> AddPenalty(PenaltyToAddDto? penaltyToAddDto);
        Task<ActionStatusDto> EditPenalty(  PenaltyToEditDto? penaltyToEditDto);
    }
}
