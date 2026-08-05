using Karim.Customer.HrApplication.Shared.DTOs.Tasks;
using task = Karim.Customer.HrApplication.Domain.Entities.Tasks.Tasks;

namespace Karim.Customer.HrApplication.Application.Specifications.Tasks
{
    internal class TasksListSpecifications : BaseSpecifications<task, string>
    {
        public TasksListSpecifications(TaskParameters parameters): base(
            TaskCriteriaCompinor.CritriaCompinor(
                TaskCriteriaCompinor.getName(parameters.Name)!,
                TaskCriteriaCompinor.getType(parameters.Type)!,
                TaskCriteriaCompinor.getStatus(parameters.Status)!,
                TaskCriteriaCompinor.getProject(parameters.ProjectId)!,
                TaskCriteriaCompinor.getTicket(parameters.TicketId)!,
                TaskCriteriaCompinor.getEmployee(parameters.EmployeeId)!
                )
            )
        {
            AddInclude(x => x.Project!);
            AddInclude(x => x.Ticket!);
            AddInclude(x => x.Employee);
            SetOrderByAsc(x => x.Code);
        }
    }
}
