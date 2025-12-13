using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department
{
    public interface IDepartmentService
    {
        public Task<ICollection<DepartmentToReturnDto>> GetDepartments(int? status, int? type);
        public ICollection<EnumDto> FillDepartmentsStatus();
        public ICollection<EnumDto> FillDepartmentTypes();
    }
}
