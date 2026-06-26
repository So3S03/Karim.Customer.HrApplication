using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal static class BonusesCriteriaGenerator
    {
        public static Expression<Func<PayrollBonus, bool>>? GetText(string? Text)
        {
            if (string.IsNullOrEmpty(Text)) return null;
            return B => B.Title.ToLower().Contains(Text.ToLower());
        }

        public static Expression<Func<PayrollBonus, bool>>? GetPayslipId(string? PayslipId)
        {
            if (string.IsNullOrEmpty(PayslipId)) return null;
            return B => B.PayslipId == PayslipId;
        }

        public static Expression<Func<PayrollBonus, bool>>? GenerateCriteria(params List<Expression<Func<PayrollBonus, bool>>>? expressions)
        {
            if (expressions is null) return null;
            if (expressions.Count(E => E is not null) == 1) expressions.Where(E => E is not null).First();
            var parameter = Expression.Parameter(typeof(PayrollBonus), "B");
            var invokedList = new List<InvocationExpression>();
            foreach (var item in expressions.Where(E => E is not null))
            {
                var expr = Expression.Invoke(item, parameter);
                invokedList.Add(expr);
            }
            var compinedExper = Expression.AndAlso(invokedList[0], invokedList[1]);
            for (var i = 2; i < expressions.Count; i++)
            {
                compinedExper = Expression.AndAlso(compinedExper, expressions[i]);
            }
            return Expression.Lambda<Func<PayrollBonus, bool>>(compinedExper, parameter);
        }
    }
}
