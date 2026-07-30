using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class RequestsStatusSpecification : BaseSpecifications<Requests, string>
    {
        public RequestsStatusSpecification(string EmpId): base(GetCritria(EmpId))
        {
            
        }

        private static Expression<Func<Requests, bool>>? GetCritria(string EmpId)
        {
            var Date = DateTime.Now;
            var StartDate = new DateOnly(Date.Year, Date.Month, 1);
            var EndDate = new DateOnly(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month));
            return R => R.StartDate >= StartDate && R.EndDate <= EndDate && R.EmpId == EmpId && R.Status == RequestStatus.Approved;
        }
    }
}
