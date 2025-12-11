using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Specifications.Department;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Services.Department
{
    internal class DepartmentServices(IUnitOfWork _UnitOfWork) : IDepartmentService
    {
        public async Task<ICollection<DepartmentToReturnDto>> GetDepartments(int type)
        {
            //checking on the modal
            if (type > 4 || type < 0) throw new Exception("Department type isn't valid");//it should be checking by error module (need implementing)
            //creating repo
            var Repo = _UnitOfWork.GenerateRepository<department, string>();
            //creating specifications
            var specs = new DepartmentsByType(type);
            //calling getAll
            var result = await Repo.GetAllAsync(specs); //returning list
            
            return new List<DepartmentToReturnDto>();
        }
    }
}