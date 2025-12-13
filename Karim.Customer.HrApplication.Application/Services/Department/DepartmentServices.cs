using Karim.Customer.HrApplication.Application._Common.EnumConverter;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Specifications.Department;
using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Mapster;
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
    }
}