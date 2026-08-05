using Karim.Customer.HrApplication.Domain.Entities.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class PayslipByEmployeeIdAndDateSpecification : BaseSpecifications<Payslip, string>
    {
        public PayslipByEmployeeIdAndDateSpecification(string employeeId, DateOnly startDate, DateOnly endDate)
            : base(P => P.EmployeeId == employeeId &&
                        P.StartDate <= endDate &&
                        P.EndDate >= startDate)
        {
        }
    }
}
