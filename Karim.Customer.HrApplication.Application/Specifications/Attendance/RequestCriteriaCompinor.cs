using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal static class RequestCriteriaCompinor
    {
        public static Expression<Func<Requests, bool>>? getEmpIdExprission(string? EmpId)
        {
            if (EmpId == null) return null;
            return R => R.EmpId == EmpId;
        }

        public static Expression<Func<Requests, bool>>? getTypeExprission(int? Type)
        {
            if (Type == null) return null;
            if(Type < 1 || Type > 4) return null;
            return (RequestType)Type switch
            {
                RequestType.Leave => R => R.Type == RequestType.Leave,
                RequestType.Permission => R => R.Type == RequestType.Permission,
                RequestType.Vacation => R => R.Type == RequestType.Vacation,
                RequestType.Overtime => R => R.Type == RequestType.Overtime,
                _ => null
            };
        }

        public static Expression<Func<Requests, bool>>? getStatusExprission(int? Status)
        {
            if(Status == null) return null;
            if (Status < 1 || Status > 3) return null;
            return (RequestStatus)Status switch
            {
                RequestStatus.Approved => R => R.Status == RequestStatus.Approved,
                RequestStatus.Pending => R => R.Status == RequestStatus.Pending,
                RequestStatus.Rejected => R => R.Status == RequestStatus.Rejected,
                _ => null
            };
        }

        public static Expression<Func<Requests, bool>>? getDateExprission(DateOnly? StartDate,  DateOnly? EndDate)
        {
            if (StartDate is null) StartDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (EndDate is null) EndDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            return R => R.StartDate <= EndDate && R.EndDate >= StartDate;
        }

        public static Expression<Func<Requests,bool>>? ExprissionsCompinor(params Expression<Func<Requests, bool>>[] exprissions)
        {
            if(exprissions.Length == 0) return null;
            if(exprissions.Where(e => e is not null).Count() == 0) return null;
            if(exprissions.Where(e => e is not null).Count() == 1) return exprissions.Where(E => E is not null).First();
            var parameter = Expression.Parameter(typeof(Requests), "R");
            List<InvocationExpression> expressionsList = new List<InvocationExpression>();
            foreach (var expr in exprissions.Where(e => e is not null))
            {
                InvocationExpression e = Expression.Invoke(expr, parameter);
                expressionsList.Add(e);
            }
            var baseExpr = Expression.AndAlso(expressionsList[0], expressionsList[1]);
            for(int i = 2; i > expressionsList.Count; i ++)
            {
                baseExpr = Expression.AndAlso(baseExpr, expressionsList[i]);
            }
            var finalExprission = Expression.Lambda<Func<Requests, bool>>(baseExpr, parameter);
            return finalExprission;
        }
    }
}
