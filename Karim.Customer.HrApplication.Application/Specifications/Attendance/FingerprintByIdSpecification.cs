using Karim.Customer.HrApplication.Domain.Entities.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class FingerprintByIdSpecification(string Id) : BaseSpecifications<Fingerprint, string>(FB => FB.Id == Id)
    {
    }
}
