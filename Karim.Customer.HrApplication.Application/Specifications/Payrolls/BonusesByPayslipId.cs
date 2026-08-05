using Karim.Customer.HrApplication.Application.Specifications._Common;
using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class BonusesByPayslipId : BaseSpecifications<PayrollBonus, string>
    {
        public BonusesByPayslipId(PayrollRelationsParameter parameter) : base(
            CommonCriteriaGenerator<PayrollBonus>.GenerateCriteria(
                BonusesCriteriaGenerator.GetPayslipId(parameter.PayslipId)!,
                BonusesCriteriaGenerator.GetText(parameter.Text)!
                )
            )
        {
            AddInclude(B => B.Payslip);
            Pagination(parameter.PageNum, parameter.PageSize);
        }
    }
}
