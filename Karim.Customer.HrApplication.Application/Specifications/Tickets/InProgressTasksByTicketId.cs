namespace Karim.Customer.HrApplication.Application.Specifications.Tickets
{
    internal class InProgressTasksByTicketId : BaseSpecifications<Domain.Entities.Tasks.Tasks, string>
    {
        public InProgressTasksByTicketId(string ticketId): base(T => T.TicketId == ticketId && T.Status != Domain.Entities.Tasks.TaskStatus.Closed)
        {
        }
    }
}
