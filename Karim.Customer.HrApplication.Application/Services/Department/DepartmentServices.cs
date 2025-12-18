using Karim.Customer.HrApplication.Application._Common.EnumConverter;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Specifications.Department;
using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using MapsterMapper;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Services.Department
{
    internal class DepartmentServices(IUnitOfWork _UnitOfWork, IMapper _mapper) : IDepartmentService
    {
        public async Task<ICollection<DepartmentToReturnDto>> GetDepartmentsAsync(int? type, string? name, int? status)
        {
            if(status == null) status = 0;
            //checking on the modal
            if (status > 4 || status < 0) throw new Exception("Department type isn't valid");//it should be checking by error module (need implementing)
            //creating repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //creating specifications
            var specs = new DepartmentsByStatusAndTypes(type, name, status);
            //calling getAll
            var result = await Repo.GetAllAsync(specs); //returning list
            //mapping the result
            var mappedDepartment = _mapper.Map<ICollection<DepartmentToReturnDto>>(result);
            return mappedDepartment;
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

        public async Task<SingleDepartmentToReturnDto> GetDepartmentByIdAsync(string? Id)
        {
            //Check on the Id
            if (Id is null) throw new Exception("the Id you have provided is not valid please provid a valid Id"); //It Should have An Error Handle
            //Create Repository
            var department = await getDepartmentAsDBEntity(Id);
            //Mapped Data
            var MappedDepartment = _mapper.Map<SingleDepartmentToReturnDto>(department);
            return MappedDepartment;
        }

        public async Task<ActionStatusDto> AddDepartmentAsync(DepartmentToAddDto? entity)
        {
            //Check on the Modal
            if (entity is null) throw new Exception("Department data you have entered is invalid");// it should be handled with error module
            //Check if the Department Code Start With (DEPT)
            if (!entity.DepartmentCode.StartsWith("DEPT")) throw new Exception("Department Code Should Start With => DEPT <= Then 3 Numbers ex: DEPT001");
            //Check if department Code Length != 7
            if (entity.DepartmentCode.Length != 7) throw new Exception("Department Code Should Be At Most 7 Character ex: DEPT001");
            //mapping form departmentToAddDto => Department
            var mappedDepartment = _mapper.Map<department>(entity);
            mappedDepartment.isActive = false;
            mappedDepartment.isRemoved = false;
            //Then Handling the Photo Upload

            //Then Get All Departments To (Check For The Department, Return it in the response)
            var AllDepartments = await this.GetDepartmentsAsync(null, null, null);
            //Check If The Department Exist
            var isExist = AllDepartments.Any(d => d.DepartmentCode == entity.DepartmentCode);
            if (isExist) throw new Exception("This Department Already Exist");
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
            if (id == null) throw new Exception("the id you have provided is invalid");
            if(!status.HasValue) throw new Exception("you should provide status for the selected department");
            //get the department
            var department = await getDepartmentAsDBEntity(id);
            //check on the department
            if (department == null) throw new Exception($"there is no department with id: {id}");
            //check if the department has the same value that exist on database
            var Message = status.Value ? "Active" : "inActive";
            if(department.isActive == status.Value) throw new Exception($"this department is already {Message}");
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

        public async Task<ActionStatusDto> UpdateDepartment(DepartmentToUpdateDto? entity)
        {
            //Check on Modal
            if (entity is null) throw new Exception("The Provided Data is Not Valid");
            if (entity.Id == null) throw new Exception("The Id is Not Valid");
            //Find Department
            var Department = await getDepartmentAsDBEntity(entity.Id);
            //Check On the department
            if (Department == null) throw new Exception($"Can't Find Department With Id: {entity.Id}");
            //Mapped Department
            var mappedDepartment = _mapper.Map(entity, Department);
            //Handling Photo

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
            if (id == null) throw new Exception("Provided Id Is InValid");
            //Get Department
            var department = await getDepartmentAsDBEntity(id);
            //Check on Department
            if (department == null) throw new Exception($"No Such Department With Id: {id}");
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


        //Commom Used Methods
        private async Task<ActionStatusDto> RemoveDepartmentToggle(string? id, bool status)
        {
            //Modal Check
            if(id is null) throw new Exception("The Provided Id Is Invalid");
            //Get The Department
            var department = await getDepartmentAsDBEntity(id);
            //Check On the department
            if (department is null) throw new Exception($"There is no department with id: {id}");
            //check if the department has the same value that exist on database
            var Message = status ? "Removed" : "Restored";
            if (department.isRemoved == status) throw new Exception($"this department is already {Message}");
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
            if (id is null) throw new Exception("Provided Id in InValid");
            //Create Repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //Create Specification Object
            var spec = new DepartmentById(id);
            //Fetch Department
            var dept = await Repo.GetByIdAsync(spec);
            //Check on the department
            if (dept is null) throw new Exception($"Department With Id: {id} is Not Found");
            return dept;
        }
    }
}