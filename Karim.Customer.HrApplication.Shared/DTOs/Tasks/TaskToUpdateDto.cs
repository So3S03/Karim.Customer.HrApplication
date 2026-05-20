namespace Karim.Customer.HrApplication.Shared.DTOs.Tasks
{
    public class TaskToUpdateDto
    {
        public required string Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required decimal AssignedHours { get; set; }
        public required string EmployeeId { get; set; }
    }
}
