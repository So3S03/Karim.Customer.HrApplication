using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class PayrollListSpecification : BaseSpecifications<Payslip, string>
    {
        public PayrollListSpecification(PayrollParameter parameter): base(
            PayrollCritiriaGenerator.GenerateCriteria(
                    PayrollCritiriaGenerator.GeneratePaymentWayCriteria(parameter.PaymentWay)!,
                    PayrollCritiriaGenerator.GenerateStatusCriteria(parameter.Status)!,
                    PayrollCritiriaGenerator.GeneratePeriodCriteria(parameter.StartDate, parameter.EndDate)!
                )
            )
        {
            AddInclude(P => P.PayrollPenalties!);   
            AddInclude(P => P.PayrollAllowances!);   
            AddInclude(P => P.PayrollBonuses!);   
            AddInclude(P => P.Employee!);
            Pagination(parameter.PageNum, parameter.PageSize);
        }
    }
}
