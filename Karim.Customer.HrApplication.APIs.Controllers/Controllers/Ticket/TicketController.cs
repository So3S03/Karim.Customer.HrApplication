using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Ticket
{
    public class TicketController(IServicesManager _servicesManager) : ApiBaseController
    {
        [HttpGet("GetAllTickets")]
        public async Task<ActionResult<DataWithPagination<ICollection<TicketToReturnDto>>>> GetAllTickets([FromQuery]TicketsParameter parameter)
        {
            var result = await _servicesManager.TicketServices.GetAllTickets(parameter);
            return Ok(result);
        }

        [HttpGet("GetTicketById")]
        public async Task<ActionResult<TicketDetailsToReturnDto>> GetTicketById(string? ticketId)
        {
            var result = await _servicesManager.TicketServices.GetSpecificTicketDetails(ticketId);
            return Ok(result);
        }

        [HttpGet("GenerateTicketMaxCode")]
        public async Task<ActionResult<MaxCodeResult>> GenerateTicketMaxCode()
        {
            var result = await _servicesManager.TicketServices.GenerateTicketCode();
            return Ok(result);
        }

        [HttpPost("AddTicket")]
        public async Task<ActionResult<ActionStatusDto>> AddTicket([FromBody]TicketToAddDto? data)
        {
            var result = await _servicesManager.TicketServices.AddNewTicket(data);
            return Ok(result);
        }

        [HttpPut("UpdateTicket")]
        public async Task<ActionResult<ActionStatusDto>> UpdateTicket([FromBody]TicketToUpdateDto? data)
        {
            var result = await _servicesManager.TicketServices.UpdateTicket(data);
            return Ok(result);
        }

        [HttpDelete("DeleteTicket")]
        public async Task<ActionResult<ActionStatusDto>> DeleteTicket(string? ticketId)
        {
            var result = await _servicesManager.TicketServices.DeleteTicket(ticketId);
            return Ok(result);
        }

        [HttpPut("ArchiveTicket")]
        public async Task<ActionResult<ActionStatusDto>> ArchiveTicket(string? ticketId)
        {
            var result = await _servicesManager.TicketServices.ArchiveTicket(ticketId);
            return Ok(result);
        }

        [HttpPut("CloseTicket")]
        public async Task<ActionResult<ActionStatusDto>> CloseTicket(string? ticketId)
        {
            var result = await _servicesManager.TicketServices.ArchiveTicket(ticketId);
            return Ok(result);
        }

        [HttpPut("UnArchiveTicket")]
        public async Task<ActionResult<ActionStatusDto>> UnArchiveTicket(string? ticketId)
        {
            var result = await _servicesManager.TicketServices.UndoArchiveTicket(ticketId);
            return Ok(result);
        }

        [HttpPut("ReOpenTicket")]
        public async Task<ActionResult<ActionStatusDto>> ReOpenTicket(string? ticketId)
        {
            var result = await _servicesManager.TicketServices.ReOpenTicket(ticketId);
            return Ok(result);
        }
    }
}
