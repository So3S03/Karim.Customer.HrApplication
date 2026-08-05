using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.Entities.Tasks;

namespace Karim.Customer.HrApplication.Domain.Entities.Tickets
{
    public class Ticket : BaseAuditableEntity<string>
    {
        public required string TicketCode { get; set; }
        public required string Name { get; set; }
        public required string NormalizedName { get; set; }
        public required TicketStatus Status { get; set; }
        public required decimal HoursNumber { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public bool IsArchive { get; set; }
        public string? Description { get; set; }
        public required string ProjectId { get; set; }
        public required Project Project { get; set; }
        public ICollection<Tasks.Tasks>? Tasks { get; set; }
    }
}
