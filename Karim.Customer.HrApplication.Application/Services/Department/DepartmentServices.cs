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
        public async Task<ICollection<DepartmentToReturnDto>> GetDepartments(int? status, int? type)
        {
            if(status == null) status = 0;
            //checking on the modal
            if (status > 4 || status < 0) throw new Exception("Department type isn't valid");//it should be checking by error module (need implementing)
            //creating repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //creating specifications
            var specs = new DepartmentsByStatusAndTypes(status, type);
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

        public async Task<SingleDepartmentToReturnDto> GetDepartmentById(string? Id)
        {
            //Check on the Id
            if (Id is null) throw new Exception("the Id you have provided is not valid please provid a valid Id"); //It Should have An Error Handle
            //Create Repository
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //Create Specification Object
            var Specs = new DepartmentById(Id);
            //Get The Department
            var Data = await Repo.GetByIdAsync(Specs);
            //Check On The Department
            if (Data is null) throw new Exception($"Department With Id {Id} Not Found"); // It Should return NotFound Response
            //Mapped Data
            var MappedDepartment = _mapper.Map<SingleDepartmentToReturnDto>(Data);
            return MappedDepartment;
        }

        public async Task<ActionStatusDto<DepartmentToReturnDto>> AddDepartment(DepartmentToAddDto? entity)
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
            var AllDepartments = await this.GetDepartments(null, null);
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
            var Obj = new ActionStatusDto<DepartmentToReturnDto>()
            {
                Status = true,
                Message = "Your Department Was Added Successfuly",
                Data = AllDepartments
            };
            return Obj;
        }
    }
}