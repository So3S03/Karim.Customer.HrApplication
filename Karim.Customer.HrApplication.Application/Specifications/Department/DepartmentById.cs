using Karim.Customer.HrApplication.Domain.Entities.Departmnet;

using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    internal class DepartmentById : BaseSpecifications<department, string>
    {
        public DepartmentById(string Id): base(d => d.Id == Id)
        {
            
        }
    }
}
