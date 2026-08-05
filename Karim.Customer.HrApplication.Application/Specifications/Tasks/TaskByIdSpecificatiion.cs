using task = Karim.Customer.HrApplication.Domain.Entities.Tasks.Tasks;

namespace Karim.Customer.HrApplication.Application.Specifications.Tasks
{
    internal class TaskByIdSpecificatiion: BaseSpecifications<task, string>
    {
        public TaskByIdSpecificatiion(string Id): base (T => T.Id == Id)
        {
            AddInclude(T => T.Project!);
            AddInclude(T => T.Employee!);
            AddInclude(T => T.Ticket!);
        }
    }
}
