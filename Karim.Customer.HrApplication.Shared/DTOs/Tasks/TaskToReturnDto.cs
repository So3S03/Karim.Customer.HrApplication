namespace Karim.Customer.HrApplication.Shared.DTOs.Tasks
{
    public class TaskToReturnDto
    {
        public required string Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required decimal TaskHours { get; set; }
        public decimal? WorkedHours { get; set; }
        public decimal? LastUsedHours { get; set; }
        public required decimal RemainingHours { get; set; }
        public DateTime? LastPull { get; set; }
        public required bool isArchived { get; set; }
        public required string Status { get; set; }
        public required string StatusId { get; set; }
        public required string Type { get; set; }
        public required int TypeId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectId { get; set; }
        public string? TicketName { get; set; }
        public string? TicketCode { get; set; }
        public string? TicketId { get; set; }
        public required string EmployeeName { get; set; }
        public required string EmployeeCode { get; set; }
        public required string EmployeeId { get; set; }
    }
}
