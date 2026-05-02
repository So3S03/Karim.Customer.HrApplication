using Karim.Customer.HrApplication.Domain.Entities.Tickets;

namespace Karim.Customer.HrApplication.Application.Specifications.Tickets
{
    internal class MaxCodeTicketSpecification : BaseSpecifications<Ticket, string>
    {
        public MaxCodeTicketSpecification()
        {
            SetOrderByDesc(T => T.TicketCode);
        }
    }
}
