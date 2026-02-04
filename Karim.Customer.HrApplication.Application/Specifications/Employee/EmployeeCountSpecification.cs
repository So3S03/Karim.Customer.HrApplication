using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class EmployeeCountSpecification : BaseSpecifications<employee, string>
    {
        public EmployeeCountSpecification(EmployeeQueryParameters parameters) : base(
                    EmployeeFuncCheckerGenerator.FuncCriteriasCompinor(
                        EmployeeFuncCheckerGenerator.generateEmployeeTypeFunc(parameters.EmployeeType)!,
                        EmployeeFuncCheckerGenerator.generateWorkTypeFunc(parameters.WorkType)!,
                        EmployeeFuncCheckerGenerator.generateEmployeeByDepartmentIdFunc(parameters.Department)!,
                        EmployeeFuncCheckerGenerator.generateContractFunc(parameters.ContractChecker)!,
                        EmployeeFuncCheckerGenerator.generateEmployeeStatusFunc(parameters.EmployeeStatus)!
                        )
            )
        {

            
        }
    }
}
