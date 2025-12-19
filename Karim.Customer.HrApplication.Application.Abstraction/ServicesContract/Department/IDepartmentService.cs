using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Microsoft.AspNetCore.Http;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department
{
    public interface IDepartmentService
    {
        public Task<ICollection<DepartmentToReturnDto>> GetDepartmentsAsync(int? type, string? name, int? status);
        public ICollection<EnumDto> FillDepartmentsStatus();
        public ICollection<EnumDto> FillDepartmentTypes();
        public Task<SingleDepartmentToReturnDto> GetDepartmentByIdAsync(string? Id);
        public Task<ActionStatusDto> AddDepartmentAsync(DepartmentToAddDto? entity, IFormFile? file);
        public Task<ActionStatusDto> DepartmentActiveToggle(string? id, bool? status);
        public Task<ActionStatusDto> SoftRemoveDepartment(string? id);
        public Task<ActionStatusDto> RestoreRemovedDepartment(string? id);
        public Task<ActionStatusDto> UpdateDepartment(DepartmentToUpdateDto? entity, IFormFile? file);
        public Task<ActionStatusDto> DeleteDepartment(string? id);
    }
    
}
