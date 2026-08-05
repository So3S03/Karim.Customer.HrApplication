using Karim.Customer.HrApplication.Domain.Entities.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class PayrollsPeYearChartSpecification : BaseSpecifications<Payslip, string>
    {
        public PayrollsPeYearChartSpecification(int year) : base(P => P.StartDate.Year == year && P.Status != PayrollStatus.Pending)
        {
            
        }
    }
}
