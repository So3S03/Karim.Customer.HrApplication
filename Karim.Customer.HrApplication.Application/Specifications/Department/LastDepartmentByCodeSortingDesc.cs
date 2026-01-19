using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    internal class LastDepartmentByCodeSortingDesc: BaseSpecifications<department, string>
    {
        public LastDepartmentByCodeSortingDesc(): base()
        {
            SetOrderByDesc(D => D.DepartmentCode);
        }
    }
}
