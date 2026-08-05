using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Tickets;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Tickets
{
    public interface ITicketServices
    {
        Task<MaxCodeResult> GenerateTicketCode();
        Task<ActionStatusDto> AddNewTicket(TicketToAddDto? data);
        Task<ActionStatusDto> UpdateTicket(TicketToUpdateDto? data);
        Task<TicketDetailsToReturnDto> GetSpecificTicketDetails(string? ticketId);
        Task<DataWithPagination<ICollection<TicketToReturnDto>>> GetAllTickets(TicketsParameter parameters);
        Task<ActionStatusDto> DeleteTicket(string? ticketId);
        Task<ActionStatusDto> ArchiveTicket(string? ticketId);
        Task<ActionStatusDto> UndoArchiveTicket(string? ticketId);
        Task<ActionStatusDto> CloseTicket(string? ticketId);
        Task<ActionStatusDto> ReOpenTicket(string? ticketId);
    }
}
