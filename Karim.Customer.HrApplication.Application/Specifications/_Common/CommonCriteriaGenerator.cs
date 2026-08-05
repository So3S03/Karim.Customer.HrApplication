using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications._Common
{
    internal static class CommonCriteriaGenerator<T> where T : class
    {
        public static Expression<Func<T, bool>>? GenerateCriteria(params List<Expression<Func<T, bool>>> expressions)
        {
            if (expressions is null) return null;

            var validExpressions = expressions.Where(E => E is not null).ToList();

            if (validExpressions.Count == 0) return null;
            if (validExpressions.Count == 1) return validExpressions[0];

            var parameter = Expression.Parameter(typeof(T), "T");

            var invokedList = validExpressions
                .Select(item => (Expression)Expression.Invoke(item, parameter))
                .ToList();

            var combinedExpr = invokedList[0];
            for (var i = 1; i < invokedList.Count; i++)
            {
                combinedExpr = Expression.AndAlso(combinedExpr, invokedList[i]);
            }

            return Expression.Lambda<Func<T, bool>>(combinedExpr, parameter);
        }
    }
}
