using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    public class EmployeeByIdSepecification : BaseSpecifications<employee, string>
    {
        public EmployeeByIdSepecification(string Id) : base(E => E.Id == Id)
        {
            AddInclude(E => E.Department!);
            AddInclude(E => E.ManagedDepartment!);
        }
    }
}
