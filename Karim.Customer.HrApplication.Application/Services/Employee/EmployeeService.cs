using Karim.Customer.HrApplication.Application._Common.EnumConverter;
using Karim.Customer.HrApplication.Application._Common.FileHandler;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Services.Employee
{
    internal class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment env) : IEmployeeService
    {
        private const string codePattern = @"^EMP\d{3,}$";
        public async Task<MaxCodeResult> GenerateEmployeeMaxCodeAsync()
        {
            //Create Repo 
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Specificatiom
            var spec = new EmployeeByCodeSortingDesc();
            //Get First Row 
            var Employee = await Repo.GetByIdAsync(spec);
            //Create Inintiate Variable For Code
            string Code = "";
            //Check On Employee 
            if (Employee is null) Code = "EMP001";
            else
            {
                //Create Variable For Saving Last Employee Code
                string extractedCode = Employee.EmployeeCode;
                //Check on This Code
                if (extractedCode == null || !Regex.IsMatch(extractedCode, codePattern)) throw new BadRequestException("Last Code Entered Is In Wrong Format!");
                //Get Numeric Value For Claculation
                int.TryParse(extractedCode.Split("P")[1], out int numaricValue);
                //Increment Numeric Value
                numaricValue = numaricValue + 1;
                //Form New Code
                Code = $"EMP{numaricValue.ToString().PadLeft(3, '0')}";
            }
            //Form Object To Return New Code
            var Obj = new MaxCodeResult()
            {
                MaxCode = Code
            };
            //Return Code
            return Obj;
        }
        public ICollection<EnumDto> EmployeeSortingLockup()
        {
            //Create Array Of The Converted Enum
            var EmployeeSortingLockups = EnumsConvertion.CreateEnumLists<EmployeeSortingLockup>();
            //Return Collection
            return EmployeeSortingLockups;
        }
        public ICollection<EnumDto> GetContractExistLockup()
        {
            //Create EnumList
            var ContractExistList = EnumsConvertion.CreateEnumLists<ContractExistLockup>();
            //return the List
            return ContractExistList;
        }
        public ICollection<EnumDto> GetEmployeeStatusLockup()
        {
            //Create List
            var EmployeeStatusList = EnumsConvertion.CreateEnumLists<EmployeeStatusLockup>();
            //Return List
            return EmployeeStatusList;
        }
        public ICollection<EnumDto> GetEmployeeTypeLockup()
        {
            //Create Lis
            var EmployeeTypeList = EnumsConvertion.CreateEnumLists<EmployeeTypeLockup>();
            //Return List
            return EmployeeTypeList;
        }
        public ICollection<EnumDto> GetEmployeeWorkTypeLockup()
        {
            //Create List
            var WorkTypeList = EnumsConvertion.CreateEnumLists<EmployeeWorkTypeLockup>();
            //Return List3
            return WorkTypeList;
        }
        public async Task<DataWithPagination<ICollection<EmployeeToReturnDto>>> GetAllEmployeeWithPaginationAsync(EmployeeQueryParameters? parameters)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Specification For Count
            var spec = new EmployeeListSpecification(parameters!);
            //Get Employee List
            IEnumerable<employee> employees = await Repo.GetAllAsync(spec);
            //Converting List Into EmployeeDto
            var mappedEmployees = _mapper.Map<ICollection<EmployeeToReturnDto>>(employees);
            //Create Pagination Data
            int pageNum = parameters!.PageNum <= 0 ? 1 : parameters.PageNum;
            int pageSize = parameters.PageSize;
            //Create Specs For Conunt
            var countSpec = new EmployeeCountSpecification(parameters);
            //Get Total Records In Database
            int totalRecord = await Repo.GetDataCountAsync(countSpec);
            decimal pages = Math.Ceiling((decimal)(totalRecord / pageSize));
            decimal nextPage = pageNum > pages ? pages : (pageNum + 1);
            //Forming Paginated Object
            var obj = new DataWithPagination<ICollection<EmployeeToReturnDto>>(pageNum, nextPage, pageSize, totalRecord, mappedEmployees);
            //return object 
            return obj;
        }
        public async Task<SpecificEmployeeToReturnDto> GetSpecificEmployeeByIdAsync(string? Id)
        {
            //Check On Id
            if (Id is null) throw new BadRequestException("The Id You Have Provided Is Not Valid");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Specs
            var Specs = new EmployeeByIdSepecification(Id);
            //Try Get The Employee
            var Employee = await Repo.GetByIdAsync(Specs);
            //Check On Employee
            if (Employee is null) throw new NotFoundException($"Employee With Id: {Id} Not Found");
            //Mapping The Employee
            var MappedEmp = _mapper.Map<SpecificEmployeeToReturnDto>(Employee);
            //Return Employee
            return MappedEmp;
        }
        public ICollection<EnumDto> GetEmployeeRankLockup()
        {
            //Creating List
            var RankList = EnumsConvertion.CreateEnumLists<EmployeeRankLockup>();
            //return Them
            return RankList;
        }
        public async Task<ActionStatusDto> AddNewEmployeeAsync(SingleEmployeeToAddDto? entity, IFormFile? Photo)
        {
            //Check On The Employee Data
            if (entity is null) throw new BadRequestException("Data You Have Provided Is Invalid");
            //Check On Employee Code
            if (entity.EmployeeCode is null || !Regex.IsMatch(entity.EmployeeCode, codePattern)) throw new BadRequestException("Emplyee Code is invalid");
            //Check On The Employee Data
            if (entity.JoinDate.HasValue && entity.JoinDate.Value > DateTime.UtcNow) throw new BadRequestException("Join Date You Have Provided Is Invalid"); //TODO: May Need Enhancement
            //Check On The Rest Of The Entity
            _ = entity switch
            {
                { FullName: null or "" } => throw new BadRequestException("Employee Name Must Be Provided"),
                { Position: null or "" } => throw new BadRequestException("Position Must Be Provided"),
                { PhoneNumber: null or "" } => throw new BadRequestException("Phone Number Must Be Provided"),
                { WorkLocation: null or "" } => throw new BadRequestException("Work Location Must Be Provided"),
                _ => entity
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Creete Specification
            var CodeCheckerSpecs = new EmployeeByCodeSpecification(entity.EmployeeCode);
            //Get Count Of Employee That Have The Samme Code
            var isEmployeeExist = (await Repo.GetDataCountAsync(CodeCheckerSpecs)) > 0;
            //Check If There Is Employee Exist With The Provided Code
            if (isEmployeeExist) throw new ConflictException($"Employee With Code {entity.EmployeeCode} Already Exist!");
            //Mapping The Employee
            var MappedEmployee = _mapper.Map<employee>(entity);
            //Deal With Photo Uploading
            if(Photo is not null)
            {
                var FilePath = await filesSaver.SaveFiles(Photo, env);
                if (FilePath is null) throw new BadRequestException("Something Went Wrong While Saving Your Photo");
                MappedEmployee.PhotoUrl = FilePath;
            }
            //Initializing Some Properties That Depend On Other Entities
            MappedEmployee.IsHasContract = false; //It Will Be True If Contract Module Will Be Ready
            MappedEmployee.EmployeeStatus = Domain.Entities.Employee.EmployeeStatus.InActive; //It Will Be Changed Based On Attendance Module
            if (entity.JoinDate is null) MappedEmployee.JoinDate = DateTime.UtcNow;
            //Add The Employee
            await Repo.AddAsync(MappedEmployee);
            //Compleate the changes
            int complete = await _unitOfWork.CompleteAsync();
            //Check On Changes
            if (complete == 0) throw new Exception("Something Went Wrong!");
            //Form Object To Return It
            var obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Was Added Successfully!"
            };
            return obj;
        }
        public async Task<ICollection<FillEntityDto<string>>> FillDepartmentsAsync(string? Name)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<department, string>();
            //Create Specifications
            var Spec = new FillDepartmentForEmpSpecification(Name);
            //Get All Department Fill
            var AllDepartments = await Repo.GetAllAsync(Spec);
            //Mapping The Department
            var mappedDepartments = _mapper.Map<ICollection<FillEntityDto<string>>>(AllDepartments);
            //Return The Data
            return mappedDepartments;
        }
    }
}
