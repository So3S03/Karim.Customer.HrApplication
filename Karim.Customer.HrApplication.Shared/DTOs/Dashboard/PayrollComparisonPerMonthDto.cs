namespace Karim.Customer.HrApplication.Shared.DTOs.Dashboard
{
    public class PayrollComparisonPerMonthDto
    {
        public required string MonthName { get; set; }
        public required decimal MonthTotalSalary { get; set; }
    }
}
