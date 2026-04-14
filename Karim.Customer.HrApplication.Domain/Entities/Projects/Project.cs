using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Domain.Entities.Projects
{
    public class Project : BaseAuditableEntity<string>
    {
        public required string ProjectCode { get; set; }
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
        public required ProjectType ProjectType { get; set; }
        public required ProjectStatus ProjectStatus { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public string? CancelationReason { get; set; }
        public required decimal CompletionPercentage { get; set; }
        public required decimal ProjectCoast { get; set; }
        public required Currancies CoastCurrency { get; set; }

        //relations
        public string? DepartmentId { get; set; }
        public department? Department { get; set; }
        public string? ContractId { get; set; }
        public Contract? Contract { get; set; }
    }
}
