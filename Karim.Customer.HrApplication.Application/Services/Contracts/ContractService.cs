using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Contracts;
using Karim.Customer.HrApplication.Application.Specifications.Contracts;
using Karim.Customer.HrApplication.Application.Specifications.Projects;
using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Contracts;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using System.Text.RegularExpressions;

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
                { ContractCode: var t} when !Regex.IsMatch(t, codePattern) => throw new BadRequestException("Contract Code Is Required"),
                { EmployeerCompanyName: null or ""} => throw new BadRequestException("Employer / Company Name Is Required"),
                { CompanyRepresentativeName: null or ""} => throw new BadRequestException("Company Representative Name Is Required"),
                { ContractEmployeeName: null or ""} => throw new BadRequestException("Contract Employee Name Is Required"),
                { NationalId: null } => throw new BadRequestException("National ID Is Required"),
                { JobTitle: null } => throw new BadRequestException("Job Title Is Required"),
                { EmployeeWorkType: var t} when !Enum.IsDefined(typeof(WorkType), t) => throw new BadRequestException("Invalid Work Type"),
                { EmpSalary: <= 0} => throw new BadRequestException("Employee Salary Must Be Greater Than 0"),
                { CurrencyType: var t } when !Enum.IsDefined(typeof(Currancies), t) => throw new BadRequestException("Invalid Currency Type"),
                { EmpId: null or ""} => throw new BadRequestException("Employee ID Is Required"),
                _ => employeeContractToAddDto
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Contract, string>();
            //Create Spec For Checking On Code If Already Exists
            var Spec = new ContractByCodeSpecification(employeeContractToAddDto.ContractCode);
            //Get Contract
            var Contract = await Repo.GetByIdAsync(Spec);
            //Check If Contract Code Already Exists
            if(Contract is not null) throw new ConflictException("Contract Code Already Exists!");
            //Map Dto To Entity
            var mappedData = _mapper.Map<EmployeeContractToAddDto, Contract>(employeeContractToAddDto);
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
    }
}
