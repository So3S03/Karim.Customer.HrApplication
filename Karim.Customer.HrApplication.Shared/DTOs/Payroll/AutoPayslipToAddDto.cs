namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class AutoPayslipToAddDto
    {
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required decimal BasicSalary { get; set; }
        public decimal? TotalOvertime { get; set; }
        public decimal NetSalary { get; set; }
        public required string EmployeeId { get; set; }
        public int EmployeeType { get; set; }
    }
}
