using Karim.Customer.HrApplication.Shared.DTOs.Department;
using System.Linq.Expressions;
using System.Security.Cryptography;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    internal class DepartmentsCountSpecification : BaseSpecifications<department, string>
    {
        public DepartmentsCountSpecification(DepartmentQueryParameters parameters) : base(
            DepartmentFuncCheckerGenerator.CompineAllFilters(
                DepartmentFuncCheckerGenerator.generateTypeFunc(parameters.Type), 
                DepartmentFuncCheckerGenerator.generateNameFunc(parameters.Name), 
                DepartmentFuncCheckerGenerator.generateStatusFunc(parameters.Status)
                )
            )
        {

        }
    }
}
