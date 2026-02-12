using Karim.Customer.HrApplication.Domain.Entities.Employee;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class EmployeesNotTerminatedSpecification() : BaseSpecifications<employee, string>(E => E.EmployeeStatus != EmployeeStatus.Terminated)
    {
    }
}
