using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using System.Linq.Expressions;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class EmployeeListSpecification : BaseSpecifications<employee, string>
    {
        public EmployeeListSpecification(EmployeeQueryParameters parameters) : base(
            EmployeeFuncCheckerGenerator.FuncCriteriasCompinor(
                EmployeeFuncCheckerGenerator.generateEmployeeTypeFunc(parameters.EmployeeType)!,
                EmployeeFuncCheckerGenerator.generateWorkTypeFunc(parameters.WorkType)!,
                EmployeeFuncCheckerGenerator.generateEmployeeByDepartmentIdFunc(parameters.Department)!,
                EmployeeFuncCheckerGenerator.generateContractFunc(parameters.ContractChecker)!,
                EmployeeFuncCheckerGenerator.generateEmployeeStatusFunc(parameters.EmployeeStatus)!,
                EmployeeFuncCheckerGenerator.generateSearchByNameFunc(parameters.Name)!
            ))
        {
            //Relation Loading
            AddInclude(E => E.Department);
            AddInclude(E => E.ManagedDepartment);
            //Sorting 
            SortingChecker(parameters.Sorting);
            //Make Paginatiion
            Pagination(parameters.PageNum, parameters.PageSize);
        }

        private void SortingChecker(int? SortingValue)
        {
            if (SortingValue is null || SortingValue.Value <= 0 || SortingValue.Value > 8) return;
            Expression<Func<employee, object>>? expression = (EmployeeSortingLockup)SortingValue switch
            {
                EmployeeSortingLockup.EmployeeCodeAsc or EmployeeSortingLockup.EmployeeCodeAsc => E => E.EmployeeCode,
                EmployeeSortingLockup.FullNameAsc or EmployeeSortingLockup.FullNameDesc => E => E.FullName,
                EmployeeSortingLockup.SalaryAsc or EmployeeSortingLockup.SalaryDesc => E => E.Salary!,
                EmployeeSortingLockup.JoinDateAsc or EmployeeSortingLockup.JoinDateDesc => E => E.JoinDate,
                _ => null
            };
            if(expression == null) return;
            if(SortingValue % 2 == 0)
            {
                SetOrderByDesc(expression);
            }
            else
            {
                SetOrderByAsc(expression);
            }
        }
    }
}
