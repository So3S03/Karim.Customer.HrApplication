using Karim.Customer.HrApplication.Domain.Entities.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal class ContractByCodeSpecification  : BaseSpecifications<Contract, string>
    {
        public ContractByCodeSpecification(string Code) : base(c => c.ContractCode == Code)
        {
            
        }
    }
}
