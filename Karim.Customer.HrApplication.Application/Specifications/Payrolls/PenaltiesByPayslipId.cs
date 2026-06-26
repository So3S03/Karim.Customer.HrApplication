using Karim.Customer.HrApplication.Application.Specifications._Common;
using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class PenaltiesByPayslipId : BaseSpecifications<PayrollPenalty, string>
    {
        public PenaltiesByPayslipId(PayrollRelationsParameter parameter): base(
                CommonCriteriaGenerator<PayrollPenalty>.GenerateCriteria(
                        GetText(parameter.Text)!,
                        GetPayslipId(parameter.PayslipId)!
                    )
            )
        {
            AddInclude(B => B.Payslip);
            Pagination(parameter.PageNum, parameter.PageSize);
        }

        public static Expression<Func<PayrollPenalty, bool>>? GetText(string? Text)
        {
            if (string.IsNullOrEmpty(Text)) return null;
            return B => B.Title.ToLower().Contains(Text.ToLower());
        }

        public static Expression<Func<PayrollPenalty, bool>>? GetPayslipId(string? PayslipId)
        {
            if (string.IsNullOrEmpty(PayslipId)) return null;
            return B => B.PayslipId == PayslipId;
        }
    }
}
