namespace Karim.Customer.HrApplication.Shared.DTOs.Tickets
{
    public class TicketDetailsToReturnDto
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
        public bool IsArchive { get; set; }
        public required string ProjectId { get; set; }
        public required string ProjectCode { get; set; }
        public required string ProjectName { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public required bool isRemoved { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
