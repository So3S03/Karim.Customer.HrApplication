namespace Karim.Customer.HrApplication.Shared.DTOs.Contracts
{
    public class ContractToReturnDto
    {
        public required string Id { get; set; }
        public required string ContractCode { get; set; }
        public required string ContractType { get; set; }
        public required string ContractStatus { get; set; }
        public required string ContractDate { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
        public required string EmployeerCompanyName { get; set; }
        public required string CompanyRepresentativeName { get; set; }
        public string? ContractEmployeeName { get; set; }
        public string? NationalId { get; set; }
        public string? JobTitle { get; set; }
        public string? EmployeeWorkType { get; set; }
        public string? WorkLocation { get; set; }
        public decimal? EmpSalary { get; set; }
        public string? ContractorName { get; set; }
        public string? ContractorScopOfWork { get; set; }
        public decimal? ContractValue { get; set; }
        public string? PaymentTerm { get; set; }
        public string? CommercialRegistrationNumber { get; set; }
        public required string CurrencyType { get; set; }
        public string? TermsAndConditions { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmpId { get; set; }
    }
}
