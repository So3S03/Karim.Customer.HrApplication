using Karim.Customer.HrApplication.Application._Common.EnumConverter;
using Karim.Customer.HrApplication.Application._Common.FileHandler;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Specifications.Department;
using Karim.Customer.HrApplication.Domain.Conttracts;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Karim.Customer.HrApplication.Shared.DTOs.Department.DepartmentToUploadBulkDtos;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Services.Department
{
    internal class DepartmentServices(IUnitOfWork _UnitOfWork, IMapper _mapper, IWebHostEnvironment env, IExcelServices excelServices) : IDepartmentService
    {
        private const string codePattern = @"^DEPT\d{3,}$";
        public async Task<DataWithPagination<ICollection<DepartmentToReturnDto>>> GetDepartmentsAsync(DepartmentQueryParameters? parameters)
        {
            //Get All Departments
            var Data = await GetDepartmentsWithoutPaginationAsync(parameters);
            //Create Specifications For Count
            var DepartmentCountSpecs = new DepartmentsCountSpecification(parameters!);
            //Get Count
            var DepartmentsCount = await _UnitOfWork.GenerateRepository<department, string>().GetDataCountAsync(DepartmentCountSpecs);
            //Make Pagination Object
            var paginatedData = new DataWithPagination<ICollection<DepartmentToReturnDto>>(
                pageNum: parameters!.PageNum,
                nextPage: parameters!.PageNum + 1,
                pageSize: Data.Count(),
                totalRecords: DepartmentsCount,
                data: Data);
            return paginatedData;
        }

        public ICollection<EnumDto> FillDepartmentsStatus()
        {
            var data = EnumsConvertion.CreateEnumLists<DepartmentStatusLockup>();
            return data;
        }

        public ICollection<EnumDto> FillDepartmentTypes()
        {
            var data = EnumsConvertion.CreateEnumLists<DepartmentTypeLockup>();
            return data;
        }

        public ICollection<EnumDto> DepartmentSortingLockUp()
        {
            var data = EnumsConvertion.CreateEnumLists<DepartmentSortingLockup>();
            return data;
        }


        public async Task<SingleDepartmentToReturnDto> GetDepartmentByIdAsync(string? Id)
        {
            //Check on the Id
            if (Id is null) throw new BadRequestException("the Id you have provided is not valid please provid a valid Id"); //It Should have An Error Handle
            //Create Repository
            var department = await getDepartmentAsDBEntity(Id);
            //Mapped Data
            var MappedDepartment = _mapper.Map<SingleDepartmentToReturnDto>(department);
            return MappedDepartment;
        }

        public async Task<ActionStatusDto> AddDepartmentAsync(DepartmentToAddDto? entity, IFormFile? file)
        {
            //Check on the Modal
            if (entity is null) throw new BadRequestException("Department data you have entered is invalid");
            //Check if the Department Code Start With (DEPT)
            if (!Regex.IsMatch(entity.DepartmentCode, codePattern)) throw new BadRequestException("Department Code Should Start With => DEPT <= Then Atleast 3 Numbers ex: DEPT001");
            //Check if department Code Length != 7
            if (entity.DepartmentCode.Length != 7) throw new BadRequestException("Department Code Should Be At Most 7 Character ex: DEPT001");
            //mapping form departmentToAddDto => Department
            var mappedDepartment = _mapper.Map<department>(entity);
            mappedDepartment.isActive = false;
            mappedDepartment.isRemoved = false;
            //Then Handling the Photo Upload
            string filePath = file is not null ? await filesSaver.SaveFiles(file, env) : "";
            mappedDepartment.DepartmentPhotoUrl = filePath;
            //Then Get All Departments To (Check For The Department, Return it in the response)
            var AllDepartments = await this.GetDepartmentsWithoutPaginationAsync(null);
            //Check If The Department Exist
            var isExist = AllDepartments.Any(d => d.DepartmentCode == entity.DepartmentCode);
            if (isExist) throw new ConflictException("A Department With This Code Already Exist");
            //Creating Repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //Add The Department
            await Repo.AddAsync(mappedDepartment);
            //Then Complete To Check if the Department Added Or Not
            var Result = await _UnitOfWork.CompleteAsync();
            //Check If The Department Added Or Not
            if (Result == 0) throw new Exception("Something Went Wrong While Adding Your Department"); //It should be handled with Error Module

            //Then Make Success Object To Return it
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Your Department Was Added Successfuly",
            };
            return Obj;
        }

        public async Task<ActionStatusDto> DepartmentActiveToggle(string? id, bool? status)
        {
            //check on the modal
            if (id == null) throw new BadRequestException("the id you have provided is invalid");
            if(!status.HasValue) throw new BadRequestException("you should provide status for the selected department");
            //get the department
            var department = await getDepartmentAsDBEntity(id);
            //check on the department
            if (department == null) throw new NotFoundException(id, "Department");
            //check if the department has the same value that exist on database
            var Message = status.Value ? "Active" : "inActive";
            if(department.isActive == status.Value) throw new ConflictException($"this department is already {Message}");
            //update the department
            department.isActive = status.Value;
            department.isRemoved = false;
            //Update department
            _UnitOfWork.GenerateRepository<department, string>().Update(department);
            //Save
            var Result = await _UnitOfWork.CompleteAsync();
            //Check on the database response
            if (Result == 0) throw new Exception("Something Went Wrong!");
            //Create Resonse
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = $"Department Seted As {Message} Successfully"
            };
            return Obj;
        }

        public async Task<ActionStatusDto> SoftRemoveDepartment(string? id)
        {
            var Result = await RemoveDepartmentToggle(id, true);
            return Result;
        }

        public async Task<ActionStatusDto> RestoreRemovedDepartment(string? id)
        {
            var Result = await RemoveDepartmentToggle(id, false);
            return Result;
        }

        public async Task<ActionStatusDto> UpdateDepartment(DepartmentToUpdateDto? entity, IFormFile? file)
        {
            //Check on Modal
            if (entity is null) throw new BadRequestException("The Provided Data is Not Valid");
            if (entity.Id == null) throw new BadRequestException("The Id is Not Valid");
            //Find Department
            var Department = await getDepartmentAsDBEntity(entity.Id);
            //Check On the department
            if (Department == null) throw new NotFoundException(entity.Id, "Department");
            //Mapped Department
            var mappedDepartment = _mapper.Map(entity, Department);
            //Handling Photo
            if(file is not null)
            {
                //Check if the Department Has Old Photo
                if (mappedDepartment.DepartmentPhotoUrl is not null)
                {
                    //Delete The Old Photo From The Server
                    var RemovingResult = filesSaver.DeleteFile(mappedDepartment.DepartmentPhotoUrl!, env);
                    //Check If Deleted
                    if (!RemovingResult) throw new Exception("Something Went Wrong While Deleting The Old Photo");
                }
                //Add The New Photo
                mappedDepartment.DepartmentPhotoUrl = await filesSaver.SaveFiles(file, env);
            }
            //Create Repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //Update Database
            Repo.Update(mappedDepartment);
            //Save Changes
            var Result = await _UnitOfWork.CompleteAsync();
            //Check On Result
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Department Updated Successfully"
            };
            return Obj;

        }

        public async Task<ActionStatusDto> DeleteDepartment(string? id)
        {
            //Check On Id
            if (id == null) throw new BadRequestException("Provided Id Is InValid");
            //Get Department
            var department = await getDepartmentAsDBEntity(id);
            //Check on Department
            if (department == null) throw new NotFoundException(id, "Department");
            //Create Repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //Delete Department
            Repo.Delete(department);
            //Save Changes
            var Result = await _UnitOfWork.CompleteAsync();
            //Check On Result
            if (Result == 0) throw new Exception("Something Went Wrong!");
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Department Deleted Successfully"
            };
            return Obj;
        }

        public async Task<ActionStatusDto> DeletePhoto(string? id)
        {
            //Check On Id
            if (id == null) throw new BadRequestException("The Id You Have Provided is InValid");
            //Get Department
            var department = await getDepartmentAsDBEntity(id);
            //Check on Department
            if (department == null) throw new NotFoundException(id, "Department");
            //Check if the Department Has Photo
            if (department.DepartmentPhotoUrl is null) throw new BadRequestException("This Department Has No Photo To Delete");
            //Delete The Photo From The Server
            //1. Delete it from server
            var RemovingResult = filesSaver.DeleteFile(department.DepartmentPhotoUrl!, env);
            //Check If Deleted
            if(!RemovingResult) throw new Exception("Something Went Wrong While Deleting The Photo");
            //2. Delete Path From Entity
            department.DepartmentPhotoUrl = null;
            //Update The Department
            _UnitOfWork.GenerateRepository<department, string>().Update(department);
            //Save Changes
            var Result = await _UnitOfWork.CompleteAsync();
            //Check On Result
            if (Result == 0) throw new Exception("Something Went Wrong While Deleting The Photo");
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Photo Deleted Successfully"
            };
            return Obj;
        }

        public ICollection<EnumDto> GetDepartmentColumns()
        {
            //Generate List Of Columns From Enum
            var ColumnList = EnumsConvertion.CreateEnumLists<DepartmentColumnsLockUp>();
            //Return The LiST
            return ColumnList;
        }

        public byte[] GenerateDepartmentTemplateExcelSheetForAddRange()
        {
            //create an object from the DepartmentToAddDTO for creating template
            var departmentObj = new DepartmentToAddBulkDto()
            {
                DepartmentCode = "ex: DEPT001",
                DepartmentName = "Department Name",
                ActualCreationDate = DateTime.UtcNow,
                DepartmentBudgetForSalaries = 1000000,
                DepartmentBudgetForTools = 2000000,
                DepartmentBudgetForTrainees = 30000000,
                DepartmentBudgetOther = 4000000,
                Description = "Department Description",
                DepatrmentType = 19,
                TotalDepartmentBudget = 5000000
            };
            var fileAsBytes = excelServices.GenerateExcelSheetTemplate<DepartmentToAddBulkDto>(departmentObj, "DepartmentTemplateForAdd");
            return fileAsBytes;
        }

        public async Task<byte[]> GenerateDepartmentsListExcelSheet()
        {
            //forming static parameter for Department
            DepartmentQueryParameters queery = new DepartmentQueryParameters();
            //get all departments
            var AllDepartments = await GetDepartmentsWithoutPaginationAsync(queery);
            //create array of bytes of the Departments
            var data = excelServices.GenerateExcelSheetForCollection(AllDepartments, "DepartmentsList");
            return data;
        }

        public async Task<ActionStatusDto> UploadBulkDepartmentsForAdd(IFormFile? file)
        {
            //Checking If File Is Null
            if (file == null) throw new BadRequestException("File Not Found Try Upload Again!");
            //Using Method That Read File
            var departmentsList = excelServices.ReadExcelSheetForCollections<DepartmentToAddBulkDto>(file);
            //Check On List
            if (departmentsList is null) throw new BadRequestException("The File Has No Departments");
            //Try Checking On The Required Properties
            var IsRequiredPropEmptyAndCodeFormatWrong = departmentsList.Where(d =>
            string.IsNullOrEmpty(d.DepartmentCode) ||
            string.IsNullOrEmpty(d.DepartmentName) ||
            d.TotalDepartmentBudget == null ||
            d.DepartmentBudgetForSalaries == null ||
            d.DepatrmentType == null ||
            d.DepatrmentType > 19 ||
            d.DepatrmentType <= 0 || !Regex.IsMatch(d.DepartmentCode,codePattern)).Any();
            if (IsRequiredPropEmptyAndCodeFormatWrong) throw new BadRequestException("There is Required Fields With No Data At This File");
            //Storing All Departmnet Codes On List
            List<string> codes = departmentsList.Select(d => d.DepartmentCode).ToList()!;
            //Forming Specification Object
            var specs = new DepartmentByCodeCountForCheck(codes);
            //Create Repo
            var repo = _UnitOfWork.GenerateRepository<department, string>();
            //Get All Departmnets That Match The Codes On File
            var matchedDepartmentCount = await repo.GetDataCountAsync(specs);
            //Check If The Department Count > 0
            if (matchedDepartmentCount > 0) throw new ConflictException($"One Or More Department Have Simmiler Codes Please Provide A Valid Codes For Your Departments");
            //map all departmnets into department
            var mappedDepartment = _mapper.Map<List<department>>(departmentsList);
            //make normalized name has value
            mappedDepartment.ForEach(D => D.NormalizedName = D.DepartmentName.ToUpper());
            //start add range
            await repo.AddRangeAsync(mappedDepartment);
            //save changes
            var result = await _UnitOfWork.CompleteAsync();
            //Check On Saving
            if (result == 0) throw new Exception("Departments Not Saved Something Went Wrong!");
            //Forming Status Object
            var obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Departments Saved Successfully!"
            };
            return obj;
        }

        public async Task<byte[]> GenerateDepartmentListExcelSheetForUpdateRange(int? columnToBeUpdated)
        {
            //Check For Column Number
            if (columnToBeUpdated is null)
                throw new BadRequestException("No Column Type Was Provided");
            if (columnToBeUpdated <= 0 || columnToBeUpdated > 9)
                throw new BadRequestException("The Column Type Provided is Invalid");
            //Create Query For Getting All Department
            var query = new DepartmentQueryParameters();
            // Get all departments
            var allDepartments = await GetDepartmentsWithoutPaginationAsync(query);

            // Map to appropriate DTO and generate Excel
            return columnToBeUpdated switch
            {
                1 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentNameUploadBulkDto>>(allDepartments), "DepartmentWithName"),
                2 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentDescriptionUploadBulkDto>>(allDepartments), "DepartmentWithDescription"),
                3 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentActualCreationDateUploadBulkDto>>(allDepartments), "DepartmentActualCreationDate"),
                4 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentTotalDepartmentBudgetUploadBulkDto>>(allDepartments), "DepartmentWithTotalBudget"),
                5 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentBudgetForSalariesUploadBulkDto>>(allDepartments), "DepartmentWithSalariesBudget"),
                6 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentBudgetForToolsUploadBulkDto>>(allDepartments), "DepartmentWithtToolsBudget"),
                7 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentBudgetForTraineesUploadBulkDto>>(allDepartments), "DepartmentWithTraineesBudget"),
                8 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepartmentBudgetOtherUploadBulkDto>>(allDepartments), "DepartmentWithBudgetOther"),
                9 => excelServices.GenerateExcelSheetForCollection(_mapper.Map<ICollection<DepatrmentTypeUploadBulkDto>>(allDepartments), "DepartmentWithDepatrmentType"),
                _ => throw new BadRequestException("Something Went Wrong! Couldn't Generate Excel Sheet")
            };
        }

        public async Task<ActionStatusDto> UploadBulkDepartmentsForUpdate(IFormFile? file, int? columnToBeUpdated)
        {
            //Check On Files
            if (file is null) throw new BadRequestException("There is No File Provided");
            // Check On Column Type
            if (columnToBeUpdated is null) throw new BadRequestException("Column Type Must Be Provided");
            //Check On Column Type
            if (columnToBeUpdated.Value <= 0 || columnToBeUpdated.Value > 9) throw new BadRequestException("Invalid Column Type");
            //Get The Excel Data On List According To The Column Type
            dynamic ExcelDepartmentList = columnToBeUpdated.Value switch
            {
                1 => excelServices.ReadExcelSheetForCollections<DepartmentNameUploadBulkDto>(file),
                2 => excelServices.ReadExcelSheetForCollections<DepartmentDescriptionUploadBulkDto>(file),
                3 => excelServices.ReadExcelSheetForCollections<DepartmentActualCreationDateUploadBulkDto>(file),
                4 => excelServices.ReadExcelSheetForCollections<DepartmentTotalDepartmentBudgetUploadBulkDto>(file),
                5 => excelServices.ReadExcelSheetForCollections<DepartmentBudgetForSalariesUploadBulkDto>(file),
                6 => excelServices.ReadExcelSheetForCollections<DepartmentBudgetForToolsUploadBulkDto>(file),
                7 => excelServices.ReadExcelSheetForCollections<DepartmentBudgetForTraineesUploadBulkDto>(file),
                8 => excelServices.ReadExcelSheetForCollections<DepartmentBudgetOtherUploadBulkDto>(file),
                9 => excelServices.ReadExcelSheetForCollections<DepatrmentTypeUploadBulkDto>(file),
                _ => throw new BadRequestException("Invalid Column Type")
            };
            //Check For Dublication
            var ExtractedCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            //Validation On Codes With Pushing Into List
            foreach (var item in ExcelDepartmentList)
            {
                //Store Code In Variable
                var code = item.DepartmentCode as string;
                //check if there is code
                if (string.IsNullOrWhiteSpace(code)) throw new BadRequestException("You Have A Record With No Department Code");
                //Check if the code follow the pattern
                if (!Regex.IsMatch(code, codePattern)) throw new BadRequestException($"The Code {code} You Have Provided Not Match The Pattern DEPT001");
                //Pushing With Dublication Check
                if (!ExtractedCodes.Add(code)) throw new BadRequestException($"There is Dublicated Department Code {code} On The File");
            }
            //Create Repo
            var repo = _UnitOfWork.GenerateRepository<department, string>();
            //Create Specification For Fetching Departments
            var specs = new DepartmentListByCode(ExtractedCodes.ToList());
            //Fetching Departments That Matches The Codes On The File
            var departmentsFromDb = await repo.GetAllAsync(specs);
            //Check If Departments Exist
            if(!departmentsFromDb.Any()) throw new NotFoundException("No Departments From The File Exist In The System");
            if (departmentsFromDb.Count() != ExcelDepartmentList.Count) throw new NotFoundException("One Or More Departments From The File Do Not Exist In The System");
            //Updating Records
            foreach (var dept in departmentsFromDb)
            {
                var excelDept = ((IEnumerable<dynamic>)ExcelDepartmentList).First(D => string.Equals(dept.DepartmentCode, D.DepartmentCode, StringComparison.OrdinalIgnoreCase));
                switch (columnToBeUpdated)
                {
                    case 1:
                        dept.DepartmentName = excelDept.DepartmentName;
                        dept.NormalizedName = excelDept.DepartmentName.ToUpper();
                        break;
                    case 2:
                        dept.Description = excelDept.Description;
                        break;
                    case 3:
                        dept.ActualCreationDate = excelDept.ActualCreationDate;
                        break;
                    case 4:
                        dept.TotalDepartmentBudget = excelDept.TotalDepartmentBudget;
                        break;
                    case 5:
                        dept.DepartmentBudgetForSalaries = excelDept.DepartmentBudgetForSalaries;
                        break;
                    case 6:
                        dept.DepartmentBudgetForTools = excelDept.DepartmentBudgetForTools;
                        break;
                    case 7:
                        dept.DepartmentBudgetForTrainees = excelDept.DepartmentBudgetForTrainees;
                        break;
                    case 8:
                        dept.DepartmentBudgetOther = excelDept.DepartmentBudgetOther;
                        break;
                    case 9:
                        dept.DepatrmentType = excelDept.DepatrmentType;
                        break;
                    default:
                        throw new BadRequestException("Invalid Column Type");
                }
            }
            repo.UpdateRange(departmentsFromDb);
            //Compleate
            var result = await _UnitOfWork.CompleteAsync();
            //Check If The Departments Have Updated
            if (result == 0) throw new Exception("Something Went Wrong, Please Try Again Later");
            //Create ActionStatus Object
            var obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Departments Updated Successfully"
            };
            return obj;
        }

        public async Task<MaxCodeResult> GenerateMaxDepartmentCode()
        {
            //Create Repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //Create Specification
            var specs = new LastDepartmentByCodeSortingDesc();
            //Get Departments Count
            var Department = await Repo.GetByIdAsync(specs);
            //Max Code var
            string Code = "";
            //Check if department exist
            if (Department is null) Code = "DEPT001";
            else
            {
                //Get Number Part In Code
                var numericPart = decimal.Parse(Department.DepartmentCode.Substring(4));
                //Increment The Number Part By 1
                var newCodeNumberPart = numericPart + 1;
                //Form The New Code
                Code = $"DEPT{newCodeNumberPart.ToString().PadLeft(3, '0')}";
            }
            //Forming The Result Object
            var obj = new MaxCodeResult()
            {
                MaxCode = Code
            };
            return obj;
        }

        //Commom Used Methods
        private async Task<ActionStatusDto> RemoveDepartmentToggle(string? id, bool status)
        {
            //Modal Check
            if(id is null) throw new BadRequestException("The Provided Id Is Invalid");
            //Get The Department
            var department = await getDepartmentAsDBEntity(id);
            //Check On the department
            if (department is null) throw new NotFoundException(id, "Department");
            //check if the department has the same value that exist on database
            var Message = status ? "Removed" : "Restored";
            if (department.isRemoved == status) throw new ConflictException($"this department is already {Message}");
            //update the department
            department.isRemoved = status;
            department.isActive = false;
            //Update Department 
            _UnitOfWork.GenerateRepository<department, string>().Update(department);
            //Save
            var Result = await _UnitOfWork.CompleteAsync();
            //Check on the database response
            if (Result == 0) throw new Exception("Something Went Wrong!");
            //Create Resonse
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = $"Department is {Message} Successfully"
            };
            return Obj;
        }

        private async Task<department> getDepartmentAsDBEntity(string? id)
        {
            //Check on Id
            if (id is null) throw new BadRequestException("Provided Id in InValid");
            //Create Repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //Create Specification Object
            var spec = new DepartmentById(id);
            //Fetch Department
            var dept = await Repo.GetByIdAsync(spec);
            //Check on the department
            if (dept is null) throw new NotFoundException(id, "Department");
            return dept;
        }

        private async Task<ICollection<DepartmentToReturnDto>> GetDepartmentsWithoutPaginationAsync(DepartmentQueryParameters? parameters)
        {
            if (parameters.Status == null) parameters.Status = 0;
            //checking on the modal
            if (parameters.Status > 4 || parameters.Status < 0) throw new BadRequestException("Department Status is Invalid");
            //creating repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //creating specifications
            var specs = new DepartmentsListSpecifications(parameters);
            //calling getAll
            var result = await Repo.GetAllAsync(specs); //returning list
            //mapping the result
            //var mappedDepartment = _mapper.Map<ICollection<DepartmentToReturnDto>>(result);
            var mappedDepartment = _mapper.Map<ICollection<DepartmentToReturnDto>>(result);
            return mappedDepartment;
        }
    }
}