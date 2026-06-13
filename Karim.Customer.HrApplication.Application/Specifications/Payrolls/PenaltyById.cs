using Karim.Customer.HrApplication.Domain.Entities.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class PenaltyById : BaseSpecifications<PayrollPenalty, string>
    {
        public PenaltyById(string Id): base(P => P.Id == Id)
        {
            AddInclude(P => P.Payslip);
        }
    }
}
