using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications._Common
{
    internal static class CommonCriteriaGenerator<T> where T : class
    {
        public static Expression<Func<T, bool>>? GenerateCriteria(params List<Expression<Func<T, bool>>> expressions)
        {
            if (expressions is null) return null;
            if (expressions.Count(E => E is not null) == 1) expressions.Where(E => E is not null).First();
            var parameter = Expression.Parameter(typeof(PayrollBonus), "T");
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
            return Expression.Lambda<Func<T, bool>>(compinedExper, parameter);
        }
    }
}
