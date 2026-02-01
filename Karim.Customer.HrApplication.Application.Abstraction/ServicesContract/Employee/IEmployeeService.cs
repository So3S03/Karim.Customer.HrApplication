using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee
{
    public interface IEmployeeService
    {
        public Task<MaxCodeResult> GenerateEmployeeMaxCode();
    }
}
