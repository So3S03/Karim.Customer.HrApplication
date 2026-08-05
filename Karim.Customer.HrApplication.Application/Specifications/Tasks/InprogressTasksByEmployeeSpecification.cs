using task = Karim.Customer.HrApplication.Domain.Entities.Tasks.Tasks;

namespace Karim.Customer.HrApplication.Application.Specifications.Tasks
{
    internal class InprogressTasksByEmployeeSpecification: BaseSpecifications<task, string>
    {
        public InprogressTasksByEmployeeSpecification(string employeeId): base(t => t.EmployeeId == employeeId && t.Status == Domain.Entities.Tasks.TaskStatus.InProgress)
        {
            
        }
    }
}
