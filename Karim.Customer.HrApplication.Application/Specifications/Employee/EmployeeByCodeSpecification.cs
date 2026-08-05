using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    public class EmployeeByCodeSpecification : BaseSpecifications<employee, string>
    {
        public EmployeeByCodeSpecification(string Code): base(E => E.EmployeeCode == Code)
        {
            
        }
    }
}
