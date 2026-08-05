using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public enum EmployeeWorkTypeLockup
    {
        [Display(Name = "Full Time")]
        FullTime = 1,
        [Display(Name = "Part Time")]
        PartTime = 2,
        [Display(Name = "Hybrid Full Time")]
        HybridFullTime = 3,
        [Display(Name = "Hybrid Part Time")]
        HybridPartTime = 4,
        [Display(Name = "Remote Full Time")]
        RemoteFullTime = 5,
        [Display(Name = "Remote Part Time")]
        RemotePartTime = 6
    }
}
