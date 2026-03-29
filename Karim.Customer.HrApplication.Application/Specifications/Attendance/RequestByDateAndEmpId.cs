using Karim.Customer.HrApplication.Domain.Entities.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class RequestByDateAndEmpId : BaseSpecifications<Requests, string>
    {
        public RequestByDateAndEmpId(string? EmpId, DateOnly? StartDate, DateOnly? EndDate) : base(R => R.EmpId == EmpId && R.StartDate == StartDate && R.EndDate == EndDate)
        {
            
        }
    }
}
