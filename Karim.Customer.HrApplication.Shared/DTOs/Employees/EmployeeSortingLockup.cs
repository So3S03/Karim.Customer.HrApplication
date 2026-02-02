using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public enum EmployeeSortingLockup
    {
        [Display(Name = "Employee Code Asc")]
        EmployeeCodeAsc = 1,
        [Display(Name = "Employee Code Desc")]
        EmployeeCodeDesc = 2,
        [Display(Name = "Name Asc")]
        FullNameAsc = 3,
        [Display(Name = "Name Desc")]
        FullNameDesc = 4,
        [Display(Name = "Salary Asc")]
        SalaryAsc = 5,
        [Display(Name = "Salary Desc")]
        SalaryDesc = 6,
        [Display(Name = "Join Date Asc")]
        JoinDateAsc = 7,
        [Display(Name = "Join Date Desc")]
        JoinDateDesc = 8,
    }
}
