using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public enum DepartmentTypeLockup
    {
        HR = 1,
        IT = 2,
        Marketing = 3,
        Sales = 4,
        Operations = 5,
        Finance = 6,
        [Display(Name = "Research & Development")]
        ResearchAndDevelopment = 7,
        [Display(Name = "Legal & Compliance")]
        LegalAndCompliance = 8,
        [Display(Name = "Customer Service")]
        CustomerService = 9,
        [Display(Name = "Product Management")]
        ProductManagement = 10,
        Administration = 11,
        [Display(Name = "Corporate Strategy")]
        CorporateStrategy = 12,
        [Display(Name = "Risk Management")]
        RiskManagement = 13,
        [Display(Name = "Internal Communications")]
        InternalCommunications = 14,
        [Display(Name = "Environmental/Social & Governance")]
        EnvironmentalOrSocialAndGovernance = 15,
        Engineering = 16,
        [Display(Name = "Plant Operations")]
        PlantOperations = 17,
        Medical = 18,
        Software = 19
    }
}
