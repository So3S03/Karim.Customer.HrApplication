namespace Karim.Customer.HrApplication.Shared.DTOs.Dashboard
{
    public class CompanyStatusToReturnDto
    {
        public int TotalEmployees { get; set; }
        public int NewHires { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalProjects { get; set; }
        public int TotalActiveProjects { get; set; }
        public decimal TotalActiveProjectsBudgets { get; set; }
        public int TotalTasks { get; set; }
        public int TotalExpiredEmployeeContracts { get; set; }
        public int TotalExpiredProjectsContracts { get; set; }
        public decimal PastMonthTotalPayrollValue { get; set; }
        public decimal CurrentMonthTotalPayrollValue { get; set; }
    }
}
