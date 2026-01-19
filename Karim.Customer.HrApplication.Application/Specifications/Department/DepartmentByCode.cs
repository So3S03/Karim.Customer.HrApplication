using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    internal class DepartmentByCode(string Code) : BaseSpecifications<department, string>(D =>D.DepartmentCode == Code)
    {

    }
}
