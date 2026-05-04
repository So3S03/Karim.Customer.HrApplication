using Karim.Customer.HrApplication.Domain.Entities.Tickets;
using Karim.Customer.HrApplication.Shared.DTOs.Tickets;

namespace Karim.Customer.HrApplication.Application.Specifications.Tickets
{
    internal class TicketListSpecification : BaseSpecifications<Ticket, string>
    {
        public TicketListSpecification(TicketsParameter parameter): base(
                TicketFuncGenerator.funcCompinor(
                        TicketFuncGenerator.getNameFunc(parameter.Name)!,
                        TicketFuncGenerator.getStatus(parameter.Status)!
                    )
            )
        {
            AddInclude(T => T.Project);
        }
    }
}
