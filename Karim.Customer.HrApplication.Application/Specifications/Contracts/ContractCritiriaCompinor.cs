using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal static class ContractCritiriaCompinor
    {
        public static Expression<Func<Contract, bool>>? TypeFunc(int? type)
        {
            if(type is null) return null;
            return (ContractType)type switch
            {
                ContractType.Employee => C => C.ContractType == ContractType.Employee,
                ContractType.Project => C => C.ContractType == ContractType.Project,
                _ => null
            };
        }

        public static Expression<Func<Contract, bool>>? StatusFunc(int? status)
        {
            if (status is null) return null;
            return (ContractStatus)status switch
            {
                ContractStatus.Draft => C => C.ContractStatus == ContractStatus.Draft,
                ContractStatus.Active => C => C.ContractStatus == ContractStatus.Active,
                ContractStatus.Expired => C => C.ContractStatus == ContractStatus.Expired,
                ContractStatus.Terminated => C => C.ContractStatus == ContractStatus.Terminated,
                ContractStatus.Cancelled => C => C.ContractStatus == ContractStatus.Cancelled,
                _ => null
            };
        }

        public static Expression<Func<Contract, bool>>? CriteriaCompinor(params List<Expression<Func<Contract, bool>>>? funcs)
        {
            if (funcs == null || funcs.Count == 0 || funcs.Where(e => e is not null).Count() == 0) return null;
            if(funcs.Where(e => e is not null).Count() == 1) return funcs.Where(e => e is not null).First();
            var parameter = Expression.Parameter(typeof(Contract), "C");
            var invocationList = new List<InvocationExpression>();
            foreach (var item in funcs.Where(e => e is not null))
            {
                var expr = Expression.Invoke(item, parameter);
                invocationList.Add(expr);
            }
            var baseComp = Expression.AndAlso(invocationList[0], invocationList[1]);
            for (int i = 2; i < invocationList.Count; i++)
            {
                baseComp = Expression.AndAlso(baseComp, invocationList[i]);
            }
            var finalExpr = Expression.Lambda<Func<Contract, bool>>(baseComp, parameter);
            return finalExpr;
        }
    }
}
