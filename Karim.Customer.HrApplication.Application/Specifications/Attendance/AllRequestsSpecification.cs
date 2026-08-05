using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class AllRequestsSpecification : BaseSpecifications<Requests, string>
    {
        public AllRequestsSpecification(RequestsParameters parameters) : base(
                RequestCriteriaCompinor.ExprissionsCompinor(
                        RequestCriteriaCompinor.getEmpIdExprission(parameters.EmpId)!,
                        RequestCriteriaCompinor.getStatusExprission(parameters.Status)!,
                        RequestCriteriaCompinor.getTypeExprission(parameters.Type)!,
                        RequestCriteriaCompinor.getDateExprission(parameters.StartDate, parameters.EndDate)!
                    )
            )
        {
            AddInclude(R => R.Employee);
            Pagination(parameters.PageNum, parameters.PageSize);
        }
    }
}
