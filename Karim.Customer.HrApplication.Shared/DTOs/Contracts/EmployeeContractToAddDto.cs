namespace Karim.Customer.HrApplication.Shared.DTOs.Contracts
{
    public class EmployeeContractToAddDto
    {
        public required string ContractCode { get; set; }
        public required DateOnly ContractDate { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required string EmployeerCompanyName { get; set; }
        public required string CompanyRepresentativeName { get; set; }
        public required string ContractEmployeeName { get; set; }
        public required string NationalId { get; set; }
        public required string JobTitle { get; set; }
        public required int EmployeeWorkType { get; set; }
        public string? WorkLocation { get; set; }
        public required decimal EmpSalary { get; set; }
        public required int CurrencyType { get; set; }
        public string? TermsAndConditions { get; set; }
        public required string EmpId { get; set; }
    }
}
