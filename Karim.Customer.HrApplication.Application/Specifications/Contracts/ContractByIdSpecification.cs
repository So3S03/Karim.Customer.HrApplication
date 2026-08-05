using Karim.Customer.HrApplication.Domain.Entities.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal class ContractByIdSpecification : BaseSpecifications<Contract, string>
    {
        public ContractByIdSpecification(string id): base(Contract => Contract.Id == id)
        {
            AddInclude(C => C.Project!);
            AddInclude(C => C.Employee!);
        }
    }
}
