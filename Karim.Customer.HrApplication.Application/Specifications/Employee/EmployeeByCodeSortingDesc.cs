using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class EmployeeByCodeSortingDesc : BaseSpecifications<employee, string>
    {
        public EmployeeByCodeSortingDesc()
        {
            SetOrderByDesc(E => E.EmployeeCode);
        }
    }
}
