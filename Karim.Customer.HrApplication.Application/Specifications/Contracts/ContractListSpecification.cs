using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Karim.Customer.HrApplication.Shared.DTOs.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal class ContractListSpecification : BaseSpecifications<Contract, string>
    {
        public ContractListSpecification(ContractParameters contractParameters) : base()
        {
            
        }
    }
}
