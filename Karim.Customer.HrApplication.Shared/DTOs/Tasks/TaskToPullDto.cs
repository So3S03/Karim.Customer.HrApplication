namespace Karim.Customer.HrApplication.Shared.DTOs.Tasks
{
    public class TaskToPullDto
    {
        public required string TaskId { get; set; }
        public decimal? TodaysWorkedHours { get; set; }
        public required string EmployeeId { get; set; }
        public required int Status { get; set; }
    }
}
