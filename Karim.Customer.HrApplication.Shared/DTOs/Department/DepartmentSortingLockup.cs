using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public enum DepartmentSortingLockup
    {
        [Display(Name = "Department Code Ascending")]
        DepartmentCodeAsc = 1,
        [Display(Name = "Department Code Descending")]
        DepartmentCodeDesc = 2,
        [Display(Name = "Department Name Ascending")]
        DepartmentNameAsc = 3,
        [Display(Name = "Department Name Descending")]
        DepartmentNameDesc = 4,
        [Display(Name = "Total Department Budget Ascending")]
        TotalDepartmentBudgetAsc = 5,
        [Display(Name = "Total Department Budget Descending")]
        TotalDepartmentBudgetDesc = 6,
        [Display(Name = "Department Budget For Salaries Ascending")]
        DepartmentBudgetForSalariesAsc = 7,
        [Display(Name = "Department Budget For Salaries Descending")]
        DepartmentBudgetForSalariesDesc = 8,
        [Display(Name = "Department Budget For Tools Ascending")]
        DepartmentBudgetForToolsAsc = 9,
        [Display(Name = "Department Budget For Tools Descending")]
        DepartmentBudgetForToolsDesc = 10,
        [Display(Name = "Department Budget For Trainees Ascending")]
        DepartmentBudgetForTraineesAsc = 11,
        [Display(Name = "Department Budget For Trainees Descending")]
        DepartmentBudgetForTraineesDesc = 12,
        [Display(Name = "Department Budget Other Ascending")]
        DepartmentBudgetOtherAsc = 13,
        [Display(Name = "Department Budget Other Descending")]
        DepartmentBudgetOtherDesc = 14,
    }
}
