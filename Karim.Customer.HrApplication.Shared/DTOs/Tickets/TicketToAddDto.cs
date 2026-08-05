namespace Karim.Customer.HrApplication.Shared.DTOs.Tickets
{
    public class TicketToAddDto
    {
        public required string TicketCode { get; set; }
        public required string Name { get; set; }
        public required int HoursNumber { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string? Description { get; set; }
        public required string ProjectId { get; set; }
    }
}
