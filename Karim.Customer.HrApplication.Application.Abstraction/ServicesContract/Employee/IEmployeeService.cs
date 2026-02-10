
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using Microsoft.AspNetCore.Http;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee
{
    public interface IEmployeeService
    {
        public Task<MaxCodeResult> GenerateEmployeeMaxCodeAsync();
        public ICollection<EnumDto> EmployeeSortingLockup();
        public ICollection<EnumDto> GetContractExistLockup();
        public ICollection<EnumDto> GetEmployeeStatusLockup();
        public ICollection<EnumDto> GetEmployeeTypeLockup();
        public ICollection<EnumDto> GetEmployeeWorkTypeLockup();
        public ICollection<EnumDto> GetEmployeeRankLockup();
        public Task<DataWithPagination<ICollection<EmployeeToReturnDto>>> GetAllEmployeeWithPaginationAsync(EmployeeQueryParameters? parameters);
        public Task<SpecificEmployeeToReturnDto> GetSpecificEmployeeByIdAsync(string? Id);
        public Task<ActionStatusDto> AddNewEmployeeAsync(SingleEmployeeToAddDto? employee, IFormFile? Photo);
        public Task<ICollection<FillEntityDto<string>>> FillDepartmentsAsync(string? Name);
        public Task<ActionStatusDto> UpdateEmployeeAsync(SingleEmployeeToUpdateDto? entity, IFormFile? Photo);
        public Task<ActionStatusDto> RemoveEmployeeTemporarly(string? Id);
        public Task<ActionStatusDto> RestoreRemovedEmployee(string? Id);
        public Task<ActionStatusDto> RemoveEmployeePermenetly(string? Id);
    }
}
