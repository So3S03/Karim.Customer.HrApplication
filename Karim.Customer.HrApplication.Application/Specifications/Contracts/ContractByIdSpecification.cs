using Karim.Customer.HrApplication.Domain.Entities.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal class ContractByIdSpecification(string id) : BaseSpecifications<Contract, string>(Contract => Contract.Id == id)
    {
    }
}
