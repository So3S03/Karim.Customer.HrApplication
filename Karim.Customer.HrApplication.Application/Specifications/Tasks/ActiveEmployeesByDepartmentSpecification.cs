namespace Karim.Customer.HrApplication.Application.Specifications.Tasks
{
    internal class ActiveEmployeesByDepartmentSpecification : BaseSpecifications<Domain.Entities.Employee.Employee, string>
    {
        public ActiveEmployeesByDepartmentSpecification(string departmentId): base(e => e.DepartmentId == departmentId && (e.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Terminated || e.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Resigned))
        {
            
        }
    }
}
