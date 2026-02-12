using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class TerminatedEmployeesSpecification() : BaseSpecifications<employee, string>(E => E.EmployeeStatus == Domain.Entities.Employee.EmployeeStatus.Terminated)
    {
    }
}
