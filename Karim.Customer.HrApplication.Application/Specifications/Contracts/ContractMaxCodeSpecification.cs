using Karim.Customer.HrApplication.Domain.Entities.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal class ContractMaxCodeSpecification : BaseSpecifications<Contract, string>
    {
        public ContractMaxCodeSpecification()
        {
            SetOrderByDesc(C => C.ContractCode);
        }
    }
}
