namespace Karim.Customer.HrApplication.Shared.DTOs.Contracts
{
    public class ProjectContractToUpdateDto
    {
        public required string Id { get; set; }
        public required string ContractCode { get; set; }
        public required DateOnly ContractDate { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required string EmployeerCompanyName { get; set; }
        public required string CompanyRepresentativeName { get; set; }
        public required string ContractorName { get; set; }
        public string? ContractorScopOfWork { get; set; }
        public required decimal ContractValue { get; set; }
        public required int PaymentTerm { get; set; }
        public required string CommercialRegistrationNumber { get; set; }
        public required int CurrencyType { get; set; }
        public string? TermsAndConditions { get; set; }
        public required string ProjectId { get; set; }
    }
}
