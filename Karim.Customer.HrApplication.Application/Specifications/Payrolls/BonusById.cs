using Karim.Customer.HrApplication.Domain.Entities.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class BonusById : BaseSpecifications<PayrollBonus, string>
    {
        public BonusById(string Id): base(B => B.Id == Id)
        {
            AddInclude(B => B.Payslip);
        }
    }
}
