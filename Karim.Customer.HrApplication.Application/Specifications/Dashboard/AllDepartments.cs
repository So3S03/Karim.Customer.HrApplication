using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class AllDepartments : BaseSpecifications<department, string>
    {
        public AllDepartments(): base()
        {
            AddInclude(D => D.Employees!);
        }
    }
}
