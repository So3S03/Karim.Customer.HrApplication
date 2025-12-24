namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public class DepartmentQueryParameters
    {
        public int? Type { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; } = 0;
        public int? PageNum { get; set; } = 1;
        public int? PageSize { get; set; } = 6;
        public int? Sorting { get; set; }
    }
}
