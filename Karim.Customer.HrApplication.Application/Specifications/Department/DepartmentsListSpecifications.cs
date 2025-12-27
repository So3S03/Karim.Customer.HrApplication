using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using System.Linq.Expressions;
using department =  Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    internal class DepartmentsListSpecifications : BaseSpecifications<department, string>
    {
        public DepartmentsListSpecifications(DepartmentQueryParameters parameters) : base(DepartmentFuncCheckerGenerator.CompineAllFilters(DepartmentFuncCheckerGenerator.generateStatusFunc(parameters.Status), DepartmentFuncCheckerGenerator.generateTypeFunc(parameters.Type), DepartmentFuncCheckerGenerator.generateNameFunc(parameters.Name)))
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
            Pagination(parameters.PageNum, parameters.PageSize);
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
