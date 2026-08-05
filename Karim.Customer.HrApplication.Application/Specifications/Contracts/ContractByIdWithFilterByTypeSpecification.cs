using Karim.Customer.HrApplication.Domain.Entities.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal class ContractByIdWithFilterByTypeSpecification : BaseSpecifications<Contract, string>
    {
        public ContractByIdWithFilterByTypeSpecification(string id, ContractType type): base(Contract => Contract.Id == id && Contract.ContractType == type)
        {
            if(type == ContractType.Employee)
            {
                AddInclude(C => C.Employee!);
            }
            else if(type == ContractType.Project)
            {
                AddInclude(C => C.Project!);
            }
        }
    }
}
