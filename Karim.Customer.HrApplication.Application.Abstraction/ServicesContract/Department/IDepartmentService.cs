using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department
{
    public interface IDepartmentService
    {
        public Task<DataWithPagination<ICollection<DepartmentToReturnDto>>> GetDepartmentsAsync(DepartmentQueryParameters? parameters);
        public ICollection<EnumDto> FillDepartmentsStatus();
        public ICollection<EnumDto> FillDepartmentTypes();
        public ICollection<EnumDto> DepartmentSortingLockUp();
        public Task<SingleDepartmentToReturnDto> GetDepartmentByIdAsync(string? Id);
        public Task<ActionStatusDto> AddDepartmentAsync(DepartmentToAddDto? entity, IFormFile? file);
        public Task<ActionStatusDto> DepartmentActiveToggle(string? id, bool? status);
        public Task<ActionStatusDto> SoftRemoveDepartment(string? id);
        public Task<ActionStatusDto> RestoreRemovedDepartment(string? id);
        public Task<ActionStatusDto> UpdateDepartment(DepartmentToUpdateDto? entity, IFormFile? file);
        public Task<ActionStatusDto> DeleteDepartment(string? id);
        public Task<ActionStatusDto> DeletePhoto(string? id);
        public ICollection<EnumDto> GetDepartmentColumns();
        public Task<byte[]> GenerateDepartmentsListExcelSheet();
        public byte[] GenerateDepartmentTemplateExcelSheetForAddRange();
        public Task<ActionStatusDto> UploadBulkDepartmentsForAdd(IFormFile? file);
        public Task<byte[]> GenerateDepartmentListExcelSheetForUpdateRange(int? columnToBeUpdated);
        public Task<ActionStatusDto> UploadBulkDepartmentsForUpdate(IFormFile? file, int? columnToBeUpdated);
        public Task<MaxCodeResult> GenerateMaxDepartmentCode();
    }
    
}
