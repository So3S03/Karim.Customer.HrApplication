using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using System.Linq.Expressions;
using department =  Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    public class DepartmentsListSpecifications : BaseSpecifications<department, string>
    {
        public DepartmentsListSpecifications(DepartmentQueryParameters parameters) : base(CombineExpressions(generateStatusFunc(parameters.Status), generateTypeFunc(parameters.Type), generateNameFunc(parameters.Name)))
        {
            //AddInclude()
            Expression<Func<department, object>> sortExprission = GetSortingValueByEnum(parameters.Sorting);
            if (parameters.Sorting % 2 != 0 || parameters.Sorting > 14) //mean it's ODD value
            {
                SetOrderByAsc(sortExprission);
            }
            else //mean its even
            {
                SetOrderByDesc(sortExprission);
            }
        }

        private static Expression<Func<department, bool>>? generateNameFunc(string? name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : d => d.NormalizedName.Contains(name.ToUpper());
        }

        private static Expression<Func<department, bool>>? generateStatusFunc(int? status)
        {
            switch (status)
            {
                case null:
                case 0:
                    return null;
                case 1:
                    return d => d.isRemoved == true;
                case 2:
                    return d => d.isRemoved == false;
                case 3:
                    return d => d.isActive == true;
                case 4:
                    return d => d.isActive == false;
                default:
                    return null;

            }
        }
        private static Expression<Func<department, bool>>? generateTypeFunc(int? type)
        {
            switch (type)
            {
                case null:
                case 0:
                    return null;
                default:
                    return d => Convert.ToInt32(d.DepatrmentType) == type;

            }
        }

        private static Expression<Func<department, bool>>? CombineExpressions(
            Expression<Func<department, bool>>? statusExpr,
            Expression<Func<department, bool>>? typeExpr,
            Expression<Func<department, bool>>? nameExpr)
        {
            var expressionsList = new[] { statusExpr, typeExpr, nameExpr }.Where(e => e is not null);
            if(expressionsList.Count() == 0) return null;
            if(expressionsList.Count() == 1) return expressionsList.First();
            var parameter = Expression.Parameter(typeof(department), "d");//create parameter of type of the entity [d = typeof(department)]
            List<InvocationExpression> invokedExpr = new List<InvocationExpression>();//return condition part like [d.isRemoved == true] and put it into List
            foreach (var expr in expressionsList)
            {
                invokedExpr.Add(Expression.Invoke(expr!, parameter));
            }
            //base combine
            BinaryExpression combined = Expression.AndAlso(invokedExpr[0], invokedExpr[1]); //Combines the two conditions with && logic [(d.isRemoved == true) && (d.DepartmentType == 5)]

            for(int i = 2; i < invokedExpr.Count; i++)
            {
                combined = Expression.AndAlso(combined, invokedExpr[i]);
            }

            return Expression.Lambda<Func<department, bool>>(combined, parameter); //return d => (d.isRemoved == true) && (d.DepartmentType == 5)
        }

        private static Expression<Func<department, object>> GetSortingValueByEnum(int? sort)
        {
            if (sort is null) sort = 0;
            var enumValue = (DepartmentSortingLockup)sort!;
            return enumValue switch
            {
                DepartmentSortingLockup.DepartmentNameAsc or DepartmentSortingLockup.DepartmentNameDesc => D => D.DepartmentName,
                DepartmentSortingLockup.DepartmentCodeAsc or DepartmentSortingLockup.DepartmentCodeDesc => D => D.DepartmentCode,
                DepartmentSortingLockup.DepartmentBudgetForTraineesAsc or DepartmentSortingLockup.DepartmentBudgetForTraineesDesc => D => D.DepartmentBudgetForTrainees,
                DepartmentSortingLockup.DepartmentBudgetForSalariesAsc or DepartmentSortingLockup.DepartmentBudgetForSalariesDesc => D => D.DepartmentBudgetForSalaries,
                DepartmentSortingLockup.DepartmentBudgetForToolsAsc or DepartmentSortingLockup.DepartmentBudgetForToolsDesc => D => D.DepartmentBudgetForTools,
                DepartmentSortingLockup.TotalDepartmentBudgetAsc or DepartmentSortingLockup.TotalDepartmentBudgetDesc => D => D.TotalDepartmentBudget,
                DepartmentSortingLockup.DepartmentBudgetOtherAsc or DepartmentSortingLockup.DepartmentBudgetOtherDesc => D => D.DepartmentBudgetOther,
                _ => D => D.Id
            };
        }
    }
}
