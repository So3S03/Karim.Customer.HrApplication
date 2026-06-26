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
        Task<ActionStatusDto> EditPenalty(PenaltyToEditDto? penaltyToEditDto);
        Task<ActionStatusDto> DeletePenalty(string? penaltyId);
        Task<ActionStatusDto> AddBonus(BonusToAddDto? bonusToAddDto);
        Task<ActionStatusDto> EditBonus(BonusToEditDto? bonusToEditDto);
        Task<ActionStatusDto> DeleteBonus(string? bonusId);
        Task<ActionStatusDto> RePendingApprovedSalary(string? payslipId);
        Task<ActionStatusDto> DeleteSalary(string? payslipId);
        Task<DataWithPagination<ICollection<PayrollBonusToReturnDto>>> PayslipBonusesGrid(PayrollRelationsParameter parameter);
        Task<DataWithPagination<ICollection<PayrollPenaltyToReturnDto>>> PayslipPenaltiesGrid(PayrollRelationsParameter parameter);
        Task<DataWithPagination<ICollection<PayrollAllowanceToReturnDto>>> PayslipAllowancesGrid(PayrollRelationsParameter parameter);
    }
}
