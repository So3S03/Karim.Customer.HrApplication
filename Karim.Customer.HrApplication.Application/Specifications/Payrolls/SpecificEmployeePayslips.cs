using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;
using System.Reflection.Metadata;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class SpecificEmployeePayslips : BaseSpecifications<Payslip, string>
    {
        public SpecificEmployeePayslips(EmployeePayslipsParameter parameter): base(P => P.EmployeeId == parameter.EmpId)
        {
            AddInclude(P => P.PayrollPenalties!);
            AddInclude(P => P.PayrollAllowances!);
            AddInclude(P => P.PayrollBonuses!);
            AddInclude(P => P.Employee!);
            Pagination(parameter.PageNum, parameter.PageSize);
        }
    }
}
