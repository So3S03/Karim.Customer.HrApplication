namespace Karim.Customer.HrApplication.Shared.DTOs.Tickets
{
    public class TicketToReturnDto
    {
        public required string Id { get; set; }
        public required string TicketCode { get; set; }
        public required string Name { get; set; }
        public required int StatusId { get; set; }
        public required string Status { get; set; }
        public required int HoursNumber { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string? Description { get; set; }
        public required string ProjectId { get; set; }
        public required string ProjectCode { get; set; }
        public required string ProjectName { get; set; }
    }
}
