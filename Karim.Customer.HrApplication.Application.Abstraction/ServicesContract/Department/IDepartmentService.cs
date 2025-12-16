using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department
{
    public interface IDepartmentService
    {
        public Task<ICollection<DepartmentToReturnDto>> GetDepartmentsAsync(int? status, int? type);
        public ICollection<EnumDto> FillDepartmentsStatus();
        public ICollection<EnumDto> FillDepartmentTypes();
        public Task<SingleDepartmentToReturnDto> GetDepartmentByIdAsync(string? Id);
        public Task<ActionStatusDto> AddDepartmentAsync(DepartmentToAddDto? entity);
        public Task<ActionStatusDto> DepartmentActiveToggle(string? id, bool? status);
        public Task<ActionStatusDto> SoftRemoveDepartment(string? id);
        public Task<ActionStatusDto> RestoreRemovedDepartment(string? id);
        public Task<ActionStatusDto> UpdateDepartment(DepartmentToUpdateDto? entity);
        public Task<ActionStatusDto> DeleteDepartment(string? id);
    }
    
}
