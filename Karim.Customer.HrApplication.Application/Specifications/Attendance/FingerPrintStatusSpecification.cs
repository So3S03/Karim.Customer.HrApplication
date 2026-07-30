using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class FingerPrintStatusSpecification : BaseSpecifications<Fingerprint, string>
    {
        public FingerPrintStatusSpecification(string EmpId): base(GetCritria(EmpId))
        {
            
        }
        private static Expression<Func<Fingerprint, bool>>? GetCritria(string EmpId)
        {
            var Date = DateTime.Now;
            var StartDate = new DateOnly(Date.Year, Date.Month, 1);
            var EndDate = new DateOnly(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month));
            return FP => FP.Date >= StartDate && FP.Date <= EndDate && FP.EmpId == EmpId;
        }
    }
}
