using Karim.Customer.HrApplication.Domain.Entities.Departmnet;

using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    public class DepartmentListByCode(List<string> codes) : BaseSpecifications<department, string>(D => codes.Contains(D.DepartmentCode))
    {

    }
}
