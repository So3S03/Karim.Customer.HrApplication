namespace Karim.Customer.HrApplication.Shared.DTOs.Contracts
{
    public class ProjectContractDetailsToReturnDto
    {
        public required string Id { get; set; }
        public required string ContractCode { get; set; }
        public required string ContractDate { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
        public required string EmployeerCompanyName { get; set; }
        public required string CompanyRepresentativeName { get; set; }
        public required string ContractorName { get; set; }
        public string? ContractorScopOfWork { get; set; }
        public required decimal ContractValue { get; set; }
        public required string PaymentTerm { get; set; }
        public required string CommercialRegistrationNumber { get; set; }
        public required string CurrencyType { get; set; }
        public string? TermsAndConditions { get; set; }
        public required string ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public required string ProjectCode { get; set; }
        public required string ContractType { get; set; }
        public required string ContractStatus { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public required bool isRemoved { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
