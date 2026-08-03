using Karim.Customer.HrApplication.Domain.Entities.Identity;

namespace Karim.Customer.HrApplication.Application.Specifications.Identity
{
    internal class RefreshTokenByHashedTokenSpecification: BaseSpecifications<RefreshToken, string>
    {
        public RefreshTokenByHashedTokenSpecification(string hashedToken): base(rt => rt.TokenHash == hashedToken)
        {
            AddInclude(rt => rt.User);
        }
    }
}
