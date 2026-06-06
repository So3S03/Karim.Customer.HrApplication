using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal static class PayrollCritiriaGenerator
    {
        public static Expression<Func<Payslip, bool>> GeneratePeriodCriteria(DateOnly? StartDate, DateOnly? EndDate)
        {
            var now = DateTime.Now;

            if (StartDate is null && EndDate is null)
                return P => P.StartDate <= new DateOnly(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month))
                         && P.EndDate >= new DateOnly(now.Year, now.Month, 1);

            var start = StartDate ?? DateOnly.MinValue;
            var end = EndDate ?? DateOnly.MaxValue;

            return P => P.StartDate <= end && P.EndDate >= start;
        }

        public static Expression<Func<Payslip, bool>>? GenerateStatusCriteria(int? Status)
        {
            if (Status is null || Status == 0) return null;
            return (PayrollStatus)Status switch
            {
                PayrollStatus.Pending => P => P.Status == PayrollStatus.Pending,
                PayrollStatus.Approved => P => P.Status == PayrollStatus.Approved,
                PayrollStatus.Paid => P => P.Status == PayrollStatus.Paid,
                _ => null
            };
        }

        public static Expression<Func<Payslip, bool>>? GeneratePaymentWayCriteria(int? PaymentWay)
        {
            if (PaymentWay is null || PaymentWay == 0) return null;
            return (PayrollPaymentWay)PaymentWay switch
            {
                PayrollPaymentWay.BankTransfer => P => P.PaymentWay == PayrollPaymentWay.BankTransfer,
                PayrollPaymentWay.Cash => P => P.PaymentWay == PayrollPaymentWay.Cash,
                PayrollPaymentWay.CryptoCurrency => P => P.PaymentWay == PayrollPaymentWay.CryptoCurrency,
                PayrollPaymentWay.Check => P => P.PaymentWay == PayrollPaymentWay.Check,
                PayrollPaymentWay.MobilePayment => P => P.PaymentWay == PayrollPaymentWay.MobilePayment,
                _ => null
            };
        }

        public static Expression<Func<Payslip, bool>>? GenerateEmployeeIdCriteria(string? EmployeeId)
        {
            if (string.IsNullOrEmpty(EmployeeId)) return null;
            return P => P.EmployeeId == EmployeeId;
        }

        public static Expression<Func<Payslip, bool>>? GenerateCriteria(params List<Expression<Func<Payslip, bool>>> Funcs)
        {
            if(Funcs.Count == 0) return null;
            if(Funcs.Where(x => x is not null).Count() == 1) return Funcs.Where(x => x is not null).First();
            var parameter = Expression.Parameter(typeof(Payslip), "P");
            var leftSideLists = new List<InvocationExpression>();
            foreach (var expr in Funcs.Where(x => x is not null))
            {
                var invokedExpr = Expression.Invoke(expr, parameter);
                leftSideLists.Add(invokedExpr);
            }
            var basedCondition = Expression.AndAlso(leftSideLists[0], leftSideLists[1]);
            for (var i = 2; i < leftSideLists.Count; i++)
            {
                basedCondition = Expression.AndAlso(basedCondition, leftSideLists[i]);
            }
            var finalCondition = Expression.Lambda<Func<Payslip, bool>>(basedCondition, parameter);
            return finalCondition;
        }
    }
}
