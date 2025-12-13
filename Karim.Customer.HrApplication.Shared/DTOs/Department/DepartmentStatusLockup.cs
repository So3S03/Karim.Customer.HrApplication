using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public enum DepartmentStatusLockup
    {
        All = 0,
        [Display(Name = "Removed")]
        isRemoved = 1,
        [Display(Name = "Not Removed")]
        isNotRemoved = 2,
        [Display(Name = "Active")]
        isActive = 3,
        [Display(Name = "Not Active")]
        isNotActive = 4
    }
}
