using Karim.Customer.HrApplication.Domain.Entities.Tickets;
using Karim.Customer.HrApplication.Shared.DTOs.Tickets;

namespace Karim.Customer.HrApplication.Application.Specifications.Tickets
{
    internal class TicketListSpecification : BaseSpecifications<Ticket, string>
    {
        public TicketListSpecification(TicketsParameter parameter): base()
        {
            
        }
    }
}
