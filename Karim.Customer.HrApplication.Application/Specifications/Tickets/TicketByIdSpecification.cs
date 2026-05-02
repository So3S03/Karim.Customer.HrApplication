using Karim.Customer.HrApplication.Domain.Entities.Tickets;

namespace Karim.Customer.HrApplication.Application.Specifications.Tickets
{
    internal class TicketByIdSpecification : BaseSpecifications<Ticket, string>
    {
        public TicketByIdSpecification(string Id): base(T => T.Id == Id)
        {
            AddInclude(T => T.Project);
        }
    }
}
