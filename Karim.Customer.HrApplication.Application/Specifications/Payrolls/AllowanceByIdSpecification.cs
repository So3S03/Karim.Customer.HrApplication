using Karim.Customer.HrApplication.Domain.Entities.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class AllowanceByIdSpecification :BaseSpecifications<PayrollAllowance, string>
    {
        public AllowanceByIdSpecification(string id): base(A => A.Id == id)
        {
            AddInclude(A => A.Payslip);
        }
    }
}
