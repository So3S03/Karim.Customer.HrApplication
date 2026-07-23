using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Contracts;
using task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Contracts
{
    public interface IContractService
    {
        Task<MaxCodeResult> GetContractCode();
        Task<ActionStatusDto> AddEmployeeContract(EmployeeContractToAddDto? employeeContractToAddDto);
        Task<ActionStatusDto> UpdateEmployeeContract(EmployeeContractToUpdateDto? employeeContractToUpdateDto);
        Task<ActionStatusDto> AddProjectContract(ProjectContractToAddDto? projectContractToAddDto);
        Task<ActionStatusDto> UpdateProjectContract(ProjectContractToUpdateDto? projectContractToUpdateDto);
        Task<ProjectContractDetailsToReturnDto> GetProjectContract(string? ContractId);
        Task<EmployeeContractDetailsToReturnDto> GetEmployeeContract(string? ContractId);
        Task<ActionStatusDto> DeleteContract(string? ContractId);
        Task<DataWithPagination<ICollection<ContractToReturnDto>>> GetAllContracts(ContractParameters parameters);
        Task<ActionStatusDto> ActivateContract(string? ContractId);
        Task<ActionStatusDto> TerminateContract(string? ContractId);
        Task<ActionStatusDto> RenewContractWithOldConditions(string? ContractId, int? AmountOfYears);
        task CheckForExpiredContracts();
    }
}
