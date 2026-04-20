using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Contracts;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Contracts
{
    public interface IContractService
    {
        Task<MaxCodeResult> GetContractCode();
        Task<ActionStatusDto> AddEmployeeContract(EmployeeContractToAddDto? employeeContractToAddDto);
        Task<ActionStatusDto> UpdateEmployeeContract(EmployeeContractToUpdateDto? employeeContractToUpdateDto);
        Task<ActionStatusDto> AddProjectContract(ProjectContractToAddDto? projectContractToAddDto);
        Task<ActionStatusDto> UpdateProjectContract(ProjectContractToUpdateDto? projectContractToUpdateDto);
    }
}
