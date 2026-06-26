using Karim.Customer.HrApplication.Application.Specifications._Common;
using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class AllowancesByPayslipId : BaseSpecifications<PayrollAllowance, string>
    {
        public AllowancesByPayslipId(PayrollRelationsParameter parameter) : base(
                CommonCriteriaGenerator<PayrollAllowance>.GenerateCriteria(
                        GetText(parameter.Text)!,
                        GetPayslipId(parameter.PayslipId)!
                    )
            )
        {
            AddInclude(B => B.Payslip);
            Pagination(parameter.PageNum, parameter.PageSize);
        }

        public static Expression<Func<PayrollAllowance, bool>>? GetText(string? Text)
        {
            if (string.IsNullOrEmpty(Text)) return null;
            return B => B.Title.ToLower().Contains(Text.ToLower());
        }

        public static Expression<Func<PayrollAllowance, bool>>? GetPayslipId(string? PayslipId)
        {
            if (string.IsNullOrEmpty(PayslipId)) return null;
            return B => B.PayslipId == PayslipId;
        }
    }
}
