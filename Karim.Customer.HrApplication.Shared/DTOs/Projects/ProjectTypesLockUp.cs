using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public enum ProjectTypesLockUp
    {
        [Display(Name = "Internal")]
        Internal = 1,
        [Display(Name = "External")]
        External = 2,
        [Display(Name = "Research & Development")]
        RnD = 3,
        [Display(Name = "Consulting")]
        Consulting = 4,
        [Display(Name = "Maintanance")]
        Maintanance = 5
    }
}
