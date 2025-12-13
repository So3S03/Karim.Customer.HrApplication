using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using System.Linq.Expressions;
using department =  Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    public class DepartmentsByStatusAndTypes : BaseSpecifications<department, string>
    {
        public DepartmentsByStatusAndTypes(int? status, int? type): base(CombineExpressions(generateStatusFunc(status), generateTypeFunc(type)))
        {
            
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
                    return d => d.isActive == true;
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
            Expression<Func<department, bool>>? typeExpr)
        {
            // If both are null, return null
            if (statusExpr == null && typeExpr == null)
                return null;

            // If only one is null, return the other
            if (statusExpr == null)
                return typeExpr;

            if (typeExpr == null)
                return statusExpr;

            // Both exist, combine them with AND
            var parameter = Expression.Parameter(typeof(department), "d"); //create parameter of type of the entity [d = typeof(department)]

            var leftBody = Expression.Invoke(statusExpr, parameter); // return condition part like [d.isRemoved == true]
            var rightBody = Expression.Invoke(typeExpr, parameter);// return condition part like [d.DepartmentType == 5]

            var combined = Expression.AndAlso(leftBody, rightBody);// Combines the two conditions with && logic [(d.isRemoved == true) && (d.DepartmentType == 5)]

            return Expression.Lambda<Func<department, bool>>(combined, parameter); //return d => (d.isRemoved == true) && (d.DepartmentType == 5)
        }
    }
}
