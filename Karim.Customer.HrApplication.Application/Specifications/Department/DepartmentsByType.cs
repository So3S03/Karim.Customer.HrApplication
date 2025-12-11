using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using department =  Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Application.Specifications.Department
{
    public class DepartmentsByType : BaseSpecifications<department, string>
    {
        public DepartmentsByType(int type): base(
        type.Equals(DepartmentLockup.All) ? (d => true) :
        type.Equals(DepartmentLockup.isRemoved) ? (d => d.isRemoved == true) :
        type.Equals(DepartmentLockup.isNotRemoved) ? (d => d.isRemoved == false) :
        type.Equals(DepartmentLockup.isActive) ? (d => d.isActive == true) :
        type.Equals(DepartmentLockup.isNotActive) ? (d => d.isActive == false) :
        (d => true)
    )
        {
            
        }
    }
}
