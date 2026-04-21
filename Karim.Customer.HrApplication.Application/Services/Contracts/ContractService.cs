using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Contracts;
using Karim.Customer.HrApplication.Application.Specifications.Contracts;
using Karim.Customer.HrApplication.Application.Specifications.Projects;
using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
using project = Karim.Customer.HrApplication.Domain.Entities.Projects.Project;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Contracts;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using System.Text.RegularExpressions;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Application.Services.Contracts
{
    internal class ContractService(IUnitOfWork _unitOfWork, IMapper _mapper) : IContractService
    {
        private const string codePattern = @"^CTR\d{3,}$";
        public async Task<MaxCodeResult> GetContractCode()
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec
            var Spec = new ContractMaxCodeSpecification();
            //Get Projects Count
            var Contract = await Repo.GetByIdAsync(Spec);
            //Create Base Code
            string Code = "CTR";
            //Create Object
            var MaxCode = new MaxCodeResult();
            //Checck If It's the first project
            if (Contract is null)
            {
                Code = $"{Code}001";
                MaxCode.MaxCode = Code;
                return MaxCode;
            }
            //Extract Code
            var ExtractedCode = Contract!.ContractCode;
            //Extract Numaric Part
            int.TryParse(ExtractedCode.Split("R")[1], out var NumericPart);
            //Compine BaseCode With Code Number
            Code = $"{Code}{(NumericPart + 1).ToString().PadLeft(3, '0')}";
            MaxCode.MaxCode = Code;
            return MaxCode;
        }
        public async Task<ActionStatusDto> AddEmployeeContract(EmployeeContractToAddDto? employeeContractToAddDto)
        {
            //Check On Data
            if (employeeContractToAddDto is null) throw new BadRequestException("Must Provide Data For Adding New Contract");
            //Check On Specific Data
            _ = employeeContractToAddDto switch
            {
                { ContractCode: var t } when !Regex.IsMatch(t, codePattern) => throw new BadRequestException("Contract Code Is Required"),
                { EmployeerCompanyName: null or "" } => throw new BadRequestException("Employer / Company Name Is Required"),
                { CompanyRepresentativeName: null or "" } => throw new BadRequestException("Company Representative Name Is Required"),
                { ContractEmployeeName: null or "" } => throw new BadRequestException("Contract Employee Name Is Required"),
                { NationalId: null } => throw new BadRequestException("National ID Is Required"),
                { JobTitle: null } => throw new BadRequestException("Job Title Is Required"),
                { EmployeeWorkType: var t } when !Enum.IsDefined(typeof(WorkType), t) => throw new BadRequestException("Invalid Work Type"),
                { EmpSalary: <= 0 } => throw new BadRequestException("Employee Salary Must Be Greater Than 0"),
                { CurrencyType: var t } when !Enum.IsDefined(typeof(Currancies), t) => throw new BadRequestException("Invalid Currency Type"),
                { EmpId: null or "" } => throw new BadRequestException("Employee ID Is Required"),
                _ => employeeContractToAddDto
            };
            //Get Employee
            var Employee = await getEmployee(employeeContractToAddDto.EmpId);
            //Check If Employee Exists
            if (Employee is null) throw new NotFoundException("Employee You Try To Add Contract For Him Is Not Found!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec For Checking On Code If Already Exists
            var Spec = new ContractByCodeSpecification(employeeContractToAddDto.ContractCode);
            //Get Contract
            var Contract = await Repo.GetByIdAsync(Spec);
            //Check If Contract Code Already Exists
            if (Contract is not null) throw new ConflictException("Contract Code Already Exists!");
            //Map Dto To Entity
            var mappedData = _mapper.Map<EmployeeContractToAddDto, Contract>(employeeContractToAddDto);
            //Add Contract
            await Repo.AddAsync(mappedData);
            //Update Employee Work Type
            Employee.WorkType = (WorkType)employeeContractToAddDto.EmployeeWorkType;
            //Create Emp Repo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Update Employee
            EmpRepo.Update(Employee);
            //Save Changes
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto
            {
                Status = true,
                Message = "Contract Added Successfully"
            };
            //Return Object
            return Obj;
        }
        public async Task<ActionStatusDto> AddProjectContract(ProjectContractToAddDto? projectContractToAddDto)
        {
            //Check On Data
            if (projectContractToAddDto is null) throw new BadRequestException("Must Provide Data For Adding New Contract");
            //Check On Specific Data
            _ = projectContractToAddDto switch
            {
                { ContractCode: var t } when !Regex.IsMatch(t, codePattern) => throw new BadRequestException("Contract Code Is Required"),
                { EmployeerCompanyName: null or "" } => throw new BadRequestException("Employer / Company Name Is Required"),
                { CompanyRepresentativeName: null or "" } => throw new BadRequestException("Company Representative Name Is Required"),
                { ContractorName: null or "" } => throw new BadRequestException("Contractor Name Is Required"),
                { ContractValue: <= 0 } => throw new BadRequestException("Contract Value Must Be Greater Than 0"),
                { CurrencyType: var t } when !Enum.IsDefined(typeof(Currancies), t) => throw new BadRequestException("Invalid Currency Type"),
                { PaymentTerm: var t } when !Enum.IsDefined(typeof(PaymentTerm), t) => throw new BadRequestException("Invalid Payment Term"),
                { CommercialRegistrationNumber: null or "" } => throw new BadRequestException("Commercial Registration Number Is Required"),
                { ProjectId: null or "" } => throw new BadRequestException("Project ID Is Required"),
                _ => projectContractToAddDto
            };
            //Get Project
            var Project = await getProject(projectContractToAddDto.ProjectId);
            //Check If Project Exists
            if (Project is null) throw new NotFoundException("Project You Try To Add Contract For It Is Not Found!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec For Checking On Code If Already Exists
            var Spec = new ContractByCodeSpecification(projectContractToAddDto.ContractCode);
            //Get Contract
            var Contract = await Repo.GetByIdAsync(Spec);
            //Check If There is Contract
            if(Contract is not null) throw new ConflictException("Contract Code Already Exists!");
            //Map Dto To Entity
            var mappedData = _mapper.Map<ProjectContractToAddDto, Contract>(projectContractToAddDto);
            //Add Contract
            await Repo.AddAsync(mappedData);
            //Save Changes
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if(!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto
            {
                Status = true,
                Message = "Contract Added Successfully"
            };
            //Return Object
            return Obj;
        }
        public async Task<ActionStatusDto> UpdateEmployeeContract(EmployeeContractToUpdateDto? employeeContractToUpdateDto)
        {
            //Check On Data
            if (employeeContractToUpdateDto is null) throw new BadRequestException("Must Provide Data For Updating Contract");
            //Check On Specific Data
            _ = employeeContractToUpdateDto switch
            {
                { Id : null or "" } => throw new BadRequestException("Contract ID Is Required"),
                { ContractCode: var t } when !Regex.IsMatch(t, codePattern) => throw new BadRequestException("Contract Code Is Required"),
                { EmployeerCompanyName: null or "" } => throw new BadRequestException("Employer / Company Name Is Required"),
                { CompanyRepresentativeName: null or "" } => throw new BadRequestException("Company Representative Name Is Required"),
                { ContractEmployeeName: null or "" } => throw new BadRequestException("Contract Employee Name Is Required"),
                { NationalId: null } => throw new BadRequestException("National ID Is Required"),
                { JobTitle: null } => throw new BadRequestException("Job Title Is Required"),
                { EmployeeWorkType: var t } when !Enum.IsDefined(typeof(WorkType), t) => throw new BadRequestException("Invalid Work Type"),
                { EmpSalary: <= 0 } => throw new BadRequestException("Employee Salary Must Be Greater Than 0"),
                { CurrencyType: var t } when !Enum.IsDefined(typeof(Currancies), t) => throw new BadRequestException("Invalid Currency Type"),
                { EmpId: null or "" } => throw new BadRequestException("Employee ID Is Required"),
                _ => employeeContractToUpdateDto
            };
            //Get Employee
            var Employee = await getEmployee(employeeContractToUpdateDto.EmpId);
            //Check If Employee Exist
            if (Employee is null) throw new NotFoundException("Employee You Try To Update Its Contract Is Not Found!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec For Getting Contract
            var Spec = new ContractByIdSpecification(employeeContractToUpdateDto.Id);
            //Get Contract
            var ExistedContract = await Repo.GetByIdAsync(Spec);
            //Check If Contract Exists
            if(ExistedContract is null) throw new NotFoundException("Contract You Try To Update Is Not Found!");
            //Check If Contract Code Are The Same
            if(ExistedContract.ContractCode != employeeContractToUpdateDto.ContractCode) throw new BadRequestException("Contract Code Can't Be Updated!");
            //Mapping Data
            var mappedData = _mapper.Map(employeeContractToUpdateDto, ExistedContract);
            //Update Contract
            Repo.Update(mappedData);
            //Check If Employee Work Type Still The Same Or Not
            if(Employee.WorkType != (WorkType)employeeContractToUpdateDto.EmployeeWorkType)
            {
                //Create Repo
                var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
                //Update Employee Work Type
                Employee.WorkType = (WorkType)employeeContractToUpdateDto.EmployeeWorkType;
                //Update Employee
                EmpRepo.Update(Employee);
            }
            //Save Changes
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if(!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto
            {
                Status = true,
                Message = "Contract Updated Successfully"
            };
            //Return Object
            return Obj;
        }
        public async Task<ActionStatusDto> UpdateProjectContract(ProjectContractToUpdateDto? projectContractToUpdateDto)
        {
            //Check On Data
            if(projectContractToUpdateDto is null) throw new BadRequestException("Must Provide Data For Updating Contract");
            //Check On Specific Data
            _ = projectContractToUpdateDto switch
            {
                { Id : null or "" } => throw new BadRequestException("Contract ID Is Required"),
                { ContractCode: var t } when !Regex.IsMatch(t, codePattern) => throw new BadRequestException("Contract Code Is Required"),
                { EmployeerCompanyName: null or "" } => throw new BadRequestException("Employer / Company Name Is Required"),
                { CompanyRepresentativeName: null or "" } => throw new BadRequestException("Company Representative Name Is Required"),
                { ContractorName: null or "" } => throw new BadRequestException("Contractor Name Is Required"),
                { ContractValue: <= 0 } => throw new BadRequestException("Contract Value Must Be Greater Than 0"),
                { CurrencyType: var t } when !Enum.IsDefined(typeof(Currancies), t) => throw new BadRequestException("Invalid Currency Type"),
                { PaymentTerm: var t } when !Enum.IsDefined(typeof(PaymentTerm), t) => throw new BadRequestException("Invalid Payment Term"),
                { CommercialRegistrationNumber: null or "" } => throw new BadRequestException("Commercial Registration Number Is Required"),
                { ProjectId: null or "" } => throw new BadRequestException("Project ID Is Required"),
                _ => projectContractToUpdateDto
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec For Getting Contract
            var Spec = new ContractByIdSpecification(projectContractToUpdateDto.Id);
            //Get Contract
            var ExistedContract = await Repo.GetByIdAsync(Spec);
            //Check If Contract Exists
            if(ExistedContract is null) throw new NotFoundException("Contract You Try To Update Is Not Found!");
            //Check If Contract Code Are The Same
            if(ExistedContract.ContractCode != projectContractToUpdateDto.ContractCode) throw new BadRequestException("Contract Code Can't Be Updated!");
            //Mapping Data
            var mappedData = _mapper.Map(projectContractToUpdateDto, ExistedContract);
            //Update Contract
            Repo.Update(mappedData);
            //Save Changes
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if(!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto
            {
                Status = true,
                Message = "Contract Updated Successfully"
            };
            //Return Object
            return Obj;
        }
        public async Task<ProjectContractDetailsToReturnDto> GetProjectContract(string? ContractId)
        {
            //Check On Id
            if (string.IsNullOrEmpty(ContractId)) throw new BadRequestException("Provided Id is Invalid");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Specification
            var Spec = new ContractByIdSpecification(ContractId);
            //Get Contract 
            var Contract = await Repo.GetByIdAsync(Spec);
            //Check On Contract
            if (Contract is null) throw new NotFoundException("Contract Not Exist!");
            //Mapping Data
            var MappedData = _mapper.Map<ProjectContractDetailsToReturnDto>(Contract);
            //Return Contract
            return MappedData;
        }
        public async Task<EmployeeContractDetailsToReturnDto> GetEmployeeContract(string? ContractId)
        {
            //Check On Data
            if (string.IsNullOrEmpty(ContractId)) throw new BadRequestException("Invalid Id!");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec
            var Spec = new ContractByIdSpecification(ContractId);
            //Get Contract
            var Contract = await Repo.GetByIdAsync(Spec);
            //Check On Contract
            if (Contract is null) throw new NotFoundException("Contract Is Not Exist!");
            //mapping data
            var MappedData = _mapper.Map<EmployeeContractDetailsToReturnDto>(Contract);
            //return data
            return MappedData;
        }
        public async Task<ActionStatusDto> DeleteContract(string? ContractId)
        {
            //Check On Contract Id
            if (string.IsNullOrEmpty(ContractId)) throw new BadRequestException("Invalid Id!");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec
            var Spec = new ContractByIdSpecification(ContractId);
            //Get Contract
            var Contract = await Repo.GetByIdAsync(Spec);
            //Chcek If THere is Contract
            if (Contract is null) throw new NotFoundException("Contract You Want To Delete Not Exist!");
            //Delete Contract
            Repo.Delete(Contract);
            //Complete
            var Copmplete = await _unitOfWork.CompleteAsync() > 0;
            //Check ON Complete
            if (!Copmplete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Deleted Successfully!"
            };
            //Return Obj
            return Obj;
        }
        public async Task<DataWithPagination<ICollection<ContractToReturnDto>>> GetAllContracts(ContractParameters parameters)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec
            var Spec = new ContractListSpecification(parameters);
            //Get List
            var contractList = await Repo.GetAllAsync(Spec);
            //mappingList
            var mappedList = _mapper.Map<ICollection<ContractToReturnDto>>(contractList);
            //Get Count
            var Count = await Repo.GetDataCountAsync(Spec);
            //Create Object
            var Obj = new DataWithPagination<ICollection<ContractToReturnDto>>(
                pageNum: parameters.PageNum,
                nextPage: parameters.PageNum + 1,
                pageSize: parameters.PageSize,
                totalRecords: Count,
                data: mappedList
                );
            //Return Data
            return Obj;
        }

        private async Task<employee?> getEmployee(string employeeId)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Spec
            var Spec = new EmployeeByIdSepecification(employeeId);
            //Get Employee
            var Employee = await Repo.GetByIdAsync(Spec);
            //Return Employee
            return Employee;
        }
        private async Task<project?> getProject(string projectId)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<project, string>();
            //Create Spec
            var Spec = new ProjectByIdSpecification(projectId);
            //Get Project
            var Project = await Repo.GetByIdAsync(Spec);
            //Return Project
            return Project;
        }
    }
}
