using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public enum EmployeeRankLockup
    {
        [Display(Name ="Intern")]
        Intern = 1,
        [Display(Name ="Fresh")]
        Fresh = 2,
        [Display(Name ="Junior")]
        Junior = 3,
        [Display(Name ="Mid Level")]
        MidLevel = 4,
        [Display(Name ="Senior")]
        Senior = 5,
        [Display(Name ="Team Leader")]
        TeamLeader = 6,
        [Display(Name ="Project Manager")]
        ProjectManager = 7,
        [Display(Name ="Manager")]
        Manager = 8,
        [Display(Name ="Director")]
        Director = 9
    }
}
