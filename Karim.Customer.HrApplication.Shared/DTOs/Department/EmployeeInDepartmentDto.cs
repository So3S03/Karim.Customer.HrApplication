namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public class EmployeeInDepartmentDto
    {
        public required string Id { get; set; }
        public required string EmpCode { get; set; }
        public required string EmpName { get; set; }
        public required string Rank { get; set; }
        public required string Position { get; set; }
        public string? Photo { get; set; }


    }
}
