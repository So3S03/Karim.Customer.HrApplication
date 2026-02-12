using Karim.Customer.HrApplication.Application._Common.DateConverter;
using Karim.Customer.HrApplication.Application._Common.EnumConverter;
using Karim.Customer.HrApplication.Application._Common.FileHandler;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;
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
            ////Force Sorting By Code
            //if (parameters!.Sorting is null) parameters.Sorting = 1;
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
            decimal nextPage = (pageNum + 1) == (pages + 1) ? (pages + 1) : (pageNum + 1);
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
            //Converting Date To An Recognaized System Pattern
            var convertedDate = DatesConverter.Connverter(entity.JoinDate);
            //Check On The Employee Data
            if (entity.JoinDate.HasValue && convertedDate > DateTime.UtcNow) throw new BadRequestException("Join Date Must Not Be Greater Than Today!");
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
            //Force Employee To Be Not Deleted
            MappedEmployee.isRemoved = false;
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
        public async Task<ActionStatusDto> UpdateEmployeeAsync(SingleEmployeeToUpdateDto? entity, IFormFile? Photo) 
        {
            //Check on The Entity
            if (entity == null) throw new BadRequestException("The Provided Data Is Invalid!");
            //Check If The Code Exist And Match The Regex
            if (entity.EmployeeCode is null || !Regex.IsMatch(entity.EmployeeCode, codePattern)) throw new BadRequestException("Code You Have Entered Is Not Valid");
            //Converting Date
            var convertedDate = DatesConverter.Connverter(entity.JoinDate);
            //Chek On Join Date
            if (entity.JoinDate is not null && convertedDate > DateTime.UtcNow) throw new BadRequestException("Join Date Must Not Be Greater Than Today!");
            //Check On The Rest Of The Validations
            _ = entity switch
            {
                { Id: null or ""} => throw new BadRequestException("Id Must Be Exist"),
                { FullName: null or ""} => throw new BadRequestException("Full Name Must Be Exist"),
                { Position: null or ""} => throw new BadRequestException("Position Must Be Exist"),
                { PhoneNumber: null or ""} => throw new BadRequestException("Phone Must Be Exist"),
                { WorkLocation: null or ""} => throw new BadRequestException("Work Location Must Be Exist"),
                _ => entity
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Get Employee If Exist
            var Employee = await getEmployeeById(entity.Id);
            //Check If The Employee Exist 
            if (Employee is null) throw new NotFoundException(entity.EmployeeCode, "Employee");
            //Check On Code if It Is The Same
            if(Employee.EmployeeCode != entity.EmployeeCode) throw new BadRequestException("The Code You Have Provided Is Not Match The Exist Code");
            //Mapping Employee
            var mappedEmployee = _mapper.Map<employee>(entity);
            //Check On Photo If Exist Update The Photo AND Delete The Old One
            if(Photo is not null)
            {
                bool isDeleted;
                //Delete Photo From Servwer
                if(Employee.PhotoUrl is not null)
                {
                    isDeleted = filesSaver.DeleteFile(Employee.PhotoUrl, env);
                    if (!isDeleted) throw new Exception("Something Went Wrong While Delete The Old Photo");
                }
                var NewPhotoUrl = await filesSaver.SaveFiles(Photo, env);
                if (string.IsNullOrEmpty(NewPhotoUrl)) throw new Exception("Something Went Wrong While Uploding The New Photo");
                mappedEmployee.PhotoUrl = NewPhotoUrl;
                //Force Employee To Be Not Deleted
                mappedEmployee.isRemoved = false;
            }
            //Update The Entity
            Repo.Update(mappedEmployee);
            //Compleate Asyns
            var isSaved = await _unitOfWork.CompleteAsync() > 0;
            //Check On It 
            if (!isSaved) throw new Exception("Something Went Wrong While Updating The Employee!");
            //Create Success Object To Return It
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Was Updated Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> RemoveEmployeeTemporarly(string? Id)
        {
            //Try Get Employee If Exist
            var Emp = await getEmployeeById(Id);
            //Check If There is Employee Exist
            if (Emp is null) throw new NotFoundException("Employee Was Not Found!");
            //Check If The Emp is Already Removed
            if (Emp.isRemoved) throw new ConflictException("Employee is Already Removed");
            //Update Employee isRemove
            Emp.isRemoved = true;
            //Change Status To Terminated
            Emp.EmployeeStatus = EmployeeStatus.Terminated;
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Update it on Database
            Repo.Update(Emp);
            //Compleate
            int complete = await _unitOfWork.CompleteAsync();
            //Check If its Saved
            if(complete == 0) throw new Exception("Something Went Wrong");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Was Removed Successfully"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> RestoreRemovedEmployee(string? Id)
        {
            //Check On Id
            if (Id is null) throw new BadRequestException("Provided Id Is Invalid");
            //get Employee
            var Emp = await getEmployeeById(Id);
            //Check On Employee
            if (Emp is null) throw new NotFoundException(Id, "Employee");
            //Check On isRemoveed
            if (!Emp.isRemoved) throw new ConflictException("You Can't Restore An Employee That Not Removed");
            //Change Employee isRemoved
            Emp.isRemoved = false;
            //Force Employee To Be InActive
            Emp.EmployeeStatus = EmployeeStatus.InActive;
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Update The Entity
            Repo.Update(Emp);
            //Copmlete
            int complete = await _unitOfWork.CompleteAsync();
            //Check On status
            if (complete == 0) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Restored Successfully"
            };
            //Return it
            return Obj;
        }
        public async Task<ActionStatusDto> RemoveEmployeePermenetly(string? Id)
        {
            // Check On Id
            if (Id is null) throw new BadRequestException("Provided Id Is Invalid");
            //get Employee
            var Emp = await getEmployeeById(Id);
            //Check On Employee
            if (Emp is null) throw new NotFoundException(Id, "Employee");
            //Delete Existing Files For Employee
            if(!string.IsNullOrEmpty(Emp.PhotoUrl))
            {
                //Delete File
                bool isDeleted = filesSaver.DeleteFile(Emp.PhotoUrl, env);
                //Check If Deleted
                if (!isDeleted) throw new Exception("Something Went Wrong While Deleting Employee Photo!");
            }
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Delete Employee
            Repo.Delete(Emp);
            //Complete
            int complete = await _unitOfWork.CompleteAsync();
            //Check On complete
            if (complete == 0) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Deleted Permenetly Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> UploadEmployeePhoto(string? Id, IFormFile? File)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Provided Id Is Invalid");
            //Check On file
            if (File is null) throw new BadRequestException("Provided Phtot Is Invalid");
            //Get Employee
            var Employee = await getEmployeeById(Id);
            //Check On Employee
            if (Employee is null) throw new NotFoundException(Id, "Employee");
            //Check On Photo If Exist
            if(Employee.PhotoUrl is not null)
            {
                //Delete The Photo And Empty The Photo Url
                bool isDeleted = filesSaver.DeleteFile(Employee.PhotoUrl, env);
                if (!isDeleted) throw new Exception("Something Went WronG While Deleting Old Photo");
                Employee.PhotoUrl = null;
            }
            //Upload New Photo
            var filePath = await filesSaver.SaveFiles(File, env);
            //Check On generated Path
            if (string.IsNullOrEmpty(filePath)) throw new Exception("Something Went Wrong While Saving Photo");
            //Set New Path
            Employee.PhotoUrl = filePath;
            //Create Repo 
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Update The Record
            Repo.Update(Employee);
            //Compleate
            bool compleate = await _unitOfWork.CompleteAsync() > 0;
            //Check If The Record Saved
            if (!compleate) throw new Exception("Something Went Wrong While Saving Photo");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Photo Uploaded Successfully"
            };
            //Return The Result
            return Obj;
        }
        public async Task<ActionStatusDto> DeleteEmployeePhoto(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Provided Id Is Invalid");
            //Get Employee
            var Emp = await getEmployeeById(Id);
            //Check On Emp
            if (Emp is null) throw new NotFoundException(Id, "Employee");
            //Check If The Emp Has Photo To Delete
            if (string.IsNullOrEmpty(Emp.PhotoUrl)) throw new NotFoundException("Couldn't Find Any Photo For This Employee");
            //Delete Photo
            bool isDeleted = filesSaver.DeleteFile(Emp.PhotoUrl, env);
            //Check If Photo Deleted
            if (!isDeleted) throw new Exception("Something Went Wrong While Deleting The Photo");
            //Force Delete Path Ferom Database
            Emp.PhotoUrl = null;
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Update Entity
            Repo.Update(Emp);
            //Compelete
            bool complete = await _unitOfWork.CompleteAsync() > 0;
            //Check If Entity Was Updated
            if (!complete) throw new Exception("Something Went Wrong!");
            //Forming An Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Photo Was Deleted Successfully!"
            };
            //Return Obj
            return Obj;
        }
        public async Task<ActionStatusDto> TerminateEmployee(string? Id, bool RequestDeleteWithTermination)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Provided Id Is Invalid!");
            //Get Employee
            var Emp = await getEmployeeById(Id);
            //Check On Employee
            if (Emp is null) throw new NotFoundException(Id, "Employee");
            //Check If Already Terminated
            if (Emp.EmployeeStatus == EmployeeStatus.Terminated) throw new ConflictException("Employee Already Terminated");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Check On If I Want To Delete Him Perminante
            if(RequestDeleteWithTermination) Repo.Delete(Emp);
            //Check On If I Want Only Termination
            if (!RequestDeleteWithTermination)
            {   //Change Status
                Emp.EmployeeStatus = EmployeeStatus.Terminated;
                Repo.Update(Emp);
            }
            //Complete
            bool Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming An Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Terminated Successfully"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> UndoTerminatedEmployee(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Provided Id is Invalid");
            //Get Emp
            var Emp = await getEmployeeById(Id);
            //Check If Emp Exist
            if (Emp is null) throw new NotFoundException(Id, "Employee");
            //Check If Employee Is Terminated
            if (Emp.EmployeeStatus != EmployeeStatus.Terminated) throw new ConflictException("Employee Already Is Not Terminated");
            //Change Status
            Emp.EmployeeStatus = EmployeeStatus.InActive;
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Update Record
            Repo.Update(Emp);
            //Complete
            bool Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check If Completed
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Restored Successfully"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> TerminateCollectiveEmployees(List<string>? Ids)
        {
            //Check On Ids
            if (Ids is null) throw new BadRequestException("You Should Provide One Or More Id");
            //Check On Duplicated Ids
            var hasDuplicatedIds = Ids.GroupBy(id => id).Any(data => data.Count() > 1);
            //Check On Duplicated Ids
            if (hasDuplicatedIds) throw new ConflictException("Duplicated Ids Exist");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Specifications
            var Specs = new EmployeesNotTerminatedSpecification();
            //Get All Employees Whose Not Terminated
            var Emps = await Repo.GetAllAsync(Specs);
            //Check If Count is 0
            if (!Emps.Any()) throw new ConflictException("You Can't Terminate Those Employees, They Already Terminated");
            //HashSet For Saving The Terminated Employee With No Duplication
            var terminatedEmps = new HashSet<employee>();
            //Get All Emps That Match With Comming Ids
            foreach (var id in Ids)
            {
                //Get Employee
                var Emp = Emps.Where(E => E.Id == id).FirstOrDefault();
                //Check If Employee Exist 
                if (Emp is null) throw new NotFoundException(id, "Employee");
                //Terminate The Employee
                Emp.EmployeeStatus = EmployeeStatus.Terminated;
                //Push EMP To The List
                terminatedEmps.Add(Emp);
            }
            //update Range Data
            Repo.UpdateRange(terminatedEmps);
            //Complete
            var complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employees Are Terminated Successfully!"
            };
            return Obj;

        }

        //Helper Methods
        private async Task<employee?> getEmployeeById(string? Id)
        {
            //Check On Id
            if (Id is null) throw new BadRequestException("Provided Id is Invalid");
            //Create Repo 
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Spec
            var Spec = new EmployeeByIdSepecification(Id);
            //get Employee
            var Employee = await Repo.GetByIdAsyncWithNoTracking(Spec);
            return Employee;

        }
        private async Task<employee?> getEmployeeByCode(string? Code)
        {
            //Check On Id
            if (Code is null) throw new BadRequestException("Provided Code is Invalid");
            //Create Repo 
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Spec
            var Spec = new EmployeeByCodeSpecification(Code);
            //get Employee
            var Employee = await Repo.GetByIdAsyncWithNoTracking(Spec);
            return Employee;

        }
    }
}
