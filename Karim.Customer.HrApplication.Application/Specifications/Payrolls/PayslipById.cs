using Karim.Customer.HrApplication.Domain.Entities.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class PayslipById : BaseSpecifications<Payslip, string>
    {
        public PayslipById(string Id): base(P => P.Id == Id)
        {
            AddInclude(P => P.Employee);
            AddInclude(P => P.PayrollAllowances!);
            AddInclude(P => P.PayrollPenalties!);
            AddInclude(P => P.PayrollBonuses!);
        }
    }
}
