using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.Entities.Tickets;

namespace Karim.Customer.HrApplication.Domain.Entities.Tasks
{
    public class Tasks: BaseAuditableEntity<string>
    {
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
        public required bool isArchived { get; set; } = false;
        public required TaskStatus Status { get; set; }
        public required TaskType Type { get; set; }
        public Project? Project { get; set; }
        public string? ProjectId { get; set; }
        public Ticket? Ticket { get; set; }
        public string? TicketId { get; set; }
        public required Employee.Employee Employee { get; set; }
        public required string EmployeeId { get; set; }
    }
}
