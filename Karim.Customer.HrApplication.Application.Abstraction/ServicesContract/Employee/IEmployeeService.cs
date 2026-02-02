
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee
{
    public interface IEmployeeService
    {
        public Task<MaxCodeResult> GenerateEmployeeMaxCode();
        public ICollection<EnumDto> EmployeeSortingLockup();
        public Task<DataWithPagination<ICollection<EmployeeToReturnDto>>> GetAllEmployeeWithPagination(EmployeeQueryParameters? parameters);
    }
}
