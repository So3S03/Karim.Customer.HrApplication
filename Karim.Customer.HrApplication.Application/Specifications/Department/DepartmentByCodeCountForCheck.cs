using department =  Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    internal class DepartmentByCodeCountForCheck(List<string> departmentsCodes) : BaseSpecifications<department, string>(D => departmentsCodes.Contains(D.DepartmentCode))
    {
    }
}
