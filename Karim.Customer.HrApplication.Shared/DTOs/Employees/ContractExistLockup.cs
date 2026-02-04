using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public enum ContractExistLockup
    {
        [Display(Name = "Employees With Contract")]
        HasContract = 1,
        [Display(Name = "Employees With No Contract")]
        HasNoContract = 2
    }
}
