using Karim.Customer.HrApplication.Domain.Entities.Tickets;

namespace Karim.Customer.HrApplication.Application.Specifications.Tickets
{
    internal class TicketByCodeSpecification : BaseSpecifications<Ticket, string>
    {
        public TicketByCodeSpecification(string code): base(t => t.TicketCode == code)
        {
            
        }
    }
}
