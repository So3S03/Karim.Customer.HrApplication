using Karim.Customer.HrApplication.Domain.Entities.Tickets;
using System;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Tickets
{
    internal static class TicketFuncGenerator
    {
        public static Expression<Func<Ticket, bool>>? getNameFunc(string? name)
        {
            if(string.IsNullOrEmpty(name)) return null;
            return T => T.Name.Contains(name);
        }

        public static Expression<Func<Ticket, bool>>? getStatus(int? status)
        {
            if(status is null) return null;
            return (TicketStatus)status switch
            {
                TicketStatus.Opened => T => T.Status == TicketStatus.Opened,
                TicketStatus.InProgres => T => T.Status == TicketStatus.InProgres,
                TicketStatus.Closed => T => T.Status == TicketStatus.Closed,
                _ => null
            };
        }

        public static Expression<Func<Ticket, bool>>? funcCompinor(params List<Expression<Func<Ticket, bool>>>? expressions)
        {
            if(expressions is null || expressions.Count == 0 || expressions.Where(e => e is not null).Count() == 0) return null;
            if(expressions.Where(e => e is not null).Count() == 1) return expressions.Where(e => e is not null).First();
            var paramaeter = Expression.Parameter(typeof(Ticket), "T");
            List<InvocationExpression> invExprs = new List<InvocationExpression>();
            foreach(var exp in expressions.Where(e => e is not null))
            {
                var invExpression = Expression.Invoke(exp, paramaeter);
                invExprs.Add(invExpression);
            }
            var baseCompinedExprs = Expression.AndAlso(invExprs[0], invExprs[1]);
            for (int i = 2; i < invExprs.Count; i++)
            {
                baseCompinedExprs = Expression.AndAlso(baseCompinedExprs, invExprs[i]);
            }
            var finalExprission = Expression.Lambda<Func<Ticket, bool>>(baseCompinedExprs, paramaeter);
            return finalExprission;
        }
    }
}
