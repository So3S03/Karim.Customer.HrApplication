using Karim.Customer.HrApplication.Domain.Entities.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class EmployeeRequestById : BaseSpecifications<Requests, string>
    {
        public EmployeeRequestById(string ReqId) : base(r => r.Id == ReqId)
        {
            AddInclude(r => r.Employee);
        }
    }
}
