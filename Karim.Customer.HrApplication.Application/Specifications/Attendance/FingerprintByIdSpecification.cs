using Karim.Customer.HrApplication.Domain.Entities.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class FingerprintByIdSpecification : BaseSpecifications<Fingerprint, string>
    {
        public FingerprintByIdSpecification(string Id): base(FB => FB.Id == Id)
        {
            AddInclude(FB => FB.Employee);
        }
    }
}
