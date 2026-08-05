using Karim.Customer.HrApplication.Domain.Entities.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class FingerprintById : BaseSpecifications<Fingerprint, string>
    {
        public FingerprintById(string Id) : base(FB => FB.Id == Id)
        {
            AddInclude(FB => FB.Employee);   
        }
    }
}
