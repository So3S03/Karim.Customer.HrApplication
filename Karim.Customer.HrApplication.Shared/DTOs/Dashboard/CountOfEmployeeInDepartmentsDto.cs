namespace Karim.Customer.HrApplication.Shared.DTOs.Dashboard
{
    public class CountOfEmployeeInDepartmentsDto
    {
        public required string DepartmentName { get; set; }
        public required int EmployeeCount { get; set; }
    }
}
