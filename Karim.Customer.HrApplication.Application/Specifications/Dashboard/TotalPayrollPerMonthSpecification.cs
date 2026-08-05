using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class TotalPayrollPerMonthSpecification : BaseSpecifications<Payslip, string>
    {
        public TotalPayrollPerMonthSpecification(int targetedMonth) : base(
            CreateCriteria(targetedMonth)
            )
        {
            
        }

        private static Expression<Func<Payslip, bool>>? CreateCriteria(int targetedMonth)
        {
            var MonthDate = DateTime.Now.AddMonths(targetedMonth);
            var StartOfMonth = new DateOnly(MonthDate.Year, MonthDate.Month, 1);
            var EndOfMonth = new DateOnly(MonthDate.Year, MonthDate.Month, DateTime.DaysInMonth(MonthDate.Year, MonthDate.Month));
            return P => P.StartDate >= StartOfMonth &&
                 P.EndDate <= EndOfMonth &&
                 P.Status == PayrollStatus.Paid;
        }
    }
}
