using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public enum ProjectStatusLockUp
    {
        [Display(Name = "Draft")]
        Draft = 1,
        [Display(Name = "Active")]
        Active = 2,
        [Display(Name = "In Progress")]
        InProgress = 3,
        [Display(Name = "On Hold")]
        OnHold = 4,
        [Display(Name = "Completed")]
        Completed = 5,
        [Display(Name = "Cancelled")]
        Cancelled = 6
    }
}
