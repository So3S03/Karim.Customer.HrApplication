using System.Linq.Expressions;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class FillDepartmentForEmpSpecification : BaseSpecifications<department, string>
    {
        public FillDepartmentForEmpSpecification(string? Name): base(SearchByNameFunc(Name))
        {
            
        }
        private static Expression<Func<department, bool>>? SearchByNameFunc(string? Name)
        {
            if(string.IsNullOrEmpty(Name)) return null;
            Expression<Func<department, bool>> expression = D => D.NormalizedName.Contains(Name.ToUpper());
            return expression;
        }
    }
}
