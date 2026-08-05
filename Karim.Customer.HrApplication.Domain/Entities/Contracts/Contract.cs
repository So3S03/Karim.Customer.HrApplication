using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Domain.Entities.Contracts
{
    public class Contract : BaseAuditableEntity<string>
    {
        public required string ContractCode { get; set; }
        public required ContractType ContractType { get; set; }
        public required ContractStatus ContractStatus { get; set; }
        public required DateOnly ContractDate { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required string EmployeerCompanyName { get; set; }
        public required string CompanyRepresentativeName { get; set; }
        public string? ContractEmployeeName { get; set; }
        public string? NationalId { get; set; }
        public string? JobTitle { get; set; }
        public WorkType? EmployeeWorkType { get; set; }
        public string? WorkLocation { get; set; }
        public decimal? EmpSalary { get; set; }
        public string? ContractorName { get; set; }
        public string? ContractorScopOfWork { get; set; }
        public decimal? ContractValue { get; set; }
        public PaymentTerm? PaymentTerm { get; set; }
        public string? CommercialRegistrationNumber { get; set; }
        public required Currancies CurrencyType { get; set; }
        public string? TermsAndConditions { get; set; }

        //Relations
        public Project? Project { get; set; }
        public string? ProjectId { get; set; }
        public Employee.Employee? Employee { get; set; }
        public string? EmpId { get; set; }
    }
}
