using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;
using System.Linq.Expressions;
using Karim.Customer.HrApplication.Domain.Entities.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    internal static class DepartmentFuncCheckerGenerator
    {
        public static Expression<Func<department, bool>>? generateTypeFunc(int? Type)
        {
            switch (Type)
            {
                case null:
                case 0:
                    return null;
                default:
                    return d => d.DepatrmentType == (DepartmentType)Type;

            }
        }

        public static Expression<Func<department, bool>>? generateNameFunc(string? Name)
        {
            if(string.IsNullOrEmpty(Name)) return null;
            return D => D.NormalizedName.Contains(Name.ToUpper());
        }

        public static Expression<Func<department, bool>>? generateStatusFunc(int? Status)
        {
            switch(Status)
            {
                case null:
                case 0:
                    return null;
                case 1:
                    return D => D.isRemoved == true;
                case 2:
                    return D => D.isRemoved == false;
                case 3:
                    return D => D.isActive == true;
                case 4:
                    return D => D.isActive == false;
                default:
                    return null;
            }
        }

        public static Expression<Func<department, bool>>? CompineAllFilters(
            Expression<Func<department, bool>>? typeFunc,
            Expression<Func<department, bool>>? NameFunc,
            Expression<Func<department, bool>>? StatusFunc
            )
        {
            //pushing them into Array for getting the null or the only func that will be applicable
            var exprissions = new[] {typeFunc, NameFunc, StatusFunc}.Where(e => e is not null);
            if( exprissions.Count() == 0) return null;
            if (exprissions.Count() == 1) return exprissions.First();
            //create parameter
            var param = Expression.Parameter(typeof(department), "D");
            //creating list of conditions
            var conditionList = new List<InvocationExpression>();
            foreach(var e in exprissions)
            {
                var condition = Expression.Invoke(e, param);
                conditionList.Add(condition);
            }
            //compine them
            var headStart = Expression.AndAlso(conditionList[0], conditionList[1]);
            //loop for getting other expressions
            for(var i = 2; i < exprissions.Count(); i++)
            {
                headStart = Expression.AndAlso(headStart, conditionList[i]);
            }
            //returning lambada exprission
            return Expression.Lambda<Func<department, bool>>(headStart, param);

        }
    }
}
