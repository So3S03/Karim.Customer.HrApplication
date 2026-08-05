using Karim.Customer.HrApplication.Domain.Entities.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class AllFingerPrintsPerYearSpecification : BaseSpecifications<Fingerprint, string>
    {
        public AllFingerPrintsPerYearSpecification(int year) : base(FP => FP.Date.Year == year)
        {
            
        }
    }
}
