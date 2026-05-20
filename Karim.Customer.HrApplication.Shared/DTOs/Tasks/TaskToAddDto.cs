namespace Karim.Customer.HrApplication.Shared.DTOs.Tasks
{
    public class TaskToAddDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required decimal AssignedHours { get; set; }
        public required int Type { get; set; }
        public string? ProjectId { get; set; }
        public string? TicketId { get; set; }
        public required string EmployeeId { get; set; }
    }
}
