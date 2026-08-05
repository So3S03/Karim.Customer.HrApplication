using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public enum EmployeeStatusLockup
    {
        [Display(Name = "Active")]
        Active = 1,
        [Display(Name = "In Active")]
        InActive = 2,
        [Display(Name = "Terminated")]
        Terminated = 3,
        [Display(Name = "Resigned")]
        Resigned = 4,
        [Display(Name = "On Leave")]
        OnLeave = 5,
        [Display(Name = "On Vacation")]
        OnVacation = 6,
        [Display(Name = "Not Terminated")]
        NotTerminated = 7,
        [Display(Name = "Is Removed")]
        IsRemoved = 8,
        [Display(Name = "Is Not Removed")]
        IsNotRemoved = 9
    }
}
