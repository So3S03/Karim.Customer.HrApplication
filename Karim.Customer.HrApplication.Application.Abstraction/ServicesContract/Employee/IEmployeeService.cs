
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee
{
    public interface IEmployeeService
    {
        public Task<MaxCodeResult> GenerateEmployeeMaxCode();
        public ICollection<EnumDto> EmployeeSortingLockup();
        public ICollection<EnumDto> GetContractExistLockup();
        public ICollection<EnumDto> GetEmployeeStatusLockup();
        public ICollection<EnumDto> GetEmployeeTypeLockup();
        public ICollection<EnumDto> GetEmployeeWorkTypeLockup();
        public Task<DataWithPagination<ICollection<EmployeeToReturnDto>>> GetAllEmployeeWithPagination(EmployeeQueryParameters? parameters);
        public Task<SpecificEmployeeToReturnDto> GetSpecificEmployeeById(string? Id);
    }
}
