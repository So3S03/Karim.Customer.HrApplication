namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public class GetAllDepartmentsParameters
    {
        public int? Type { get; set; }
        public int? Status { get; set; } = 0;
    }
}
