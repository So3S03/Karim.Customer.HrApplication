using Karim.Customer.HrApplication.Shared.DTOs.Department;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department
{
    public interface IDepartmentService
    {
        public Task<ICollection<DepartmentToReturnDto>> GetDepartments(int? type);
    }
}
