namespace Karim.Customer.HrApplication.Shared.DTOs.Contracts
{
    public class EmployeeContractDetailsToReturnDto
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
        public required string ContractEmployeeName { get; set; }
        public required string NationalId { get; set; }
        public required string JobTitle { get; set; }
        public required string EmployeeWorkType { get; set; }
        public string? WorkLocation { get; set; }
        public required decimal EmpSalary { get; set; }
        public required string CurrencyType { get; set; }
        public string? TermsAndConditions { get; set; }
        public required string EmpId { get; set; }
        public required string EmployeeName { get; set; }
        public required string EmployeeCode { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public required bool isRemoved { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
