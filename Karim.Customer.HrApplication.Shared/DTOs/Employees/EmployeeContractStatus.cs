using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public enum EmployeeContractStatus
    {
        [Display(Name = "New Hire")]
        NewHire = 1,
        [Display(Name = "On Going")]
        OnGoing = 2,
        [Display(Name = "Near To Expire")]
        NearToExpire = 3,
        [Display(Name = "Contract Expired")]
        ContractExpired = 4,
        [Display(Name = "No Contract")]
        NoContract = 5
    }
}
