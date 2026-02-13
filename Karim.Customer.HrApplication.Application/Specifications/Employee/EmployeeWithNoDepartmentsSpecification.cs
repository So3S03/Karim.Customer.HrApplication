using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class EmployeeWithNoDepartmentsSpecification() : BaseSpecifications<employee, string>(E => E.DepartmentId == null)
    {
    }
}
