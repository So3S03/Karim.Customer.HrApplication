namespace Karim.Customer.HrApplication.Shared.DTOs.Tickets
{
    public class TicketToUpdateDto
    {
        public required string Id { get; set; }
        public required string TicketCode { get; set; }
        public required string Name { get; set; }
        public required int HoursNumber { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string? Description { get; set; }
    }
}
