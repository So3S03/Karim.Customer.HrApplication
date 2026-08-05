using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public enum EmployeeTypeLockup
    {
        [Display(Name = "Contract")]
        Contract = 1,
        [Display(Name = "Freelance")]
        Freelance = 2,
        [Display(Name = "LongLife")]
        LongLife = 3
    }
}
