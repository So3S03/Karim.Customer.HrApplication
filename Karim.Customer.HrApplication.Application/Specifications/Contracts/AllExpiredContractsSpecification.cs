using Karim.Customer.HrApplication.Domain.Entities.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Contracts
{
    internal class AllExpiredContractsSpecification : BaseSpecifications<Contract, string>
    {
        public AllExpiredContractsSpecification() : base(C =>
            DateOnly.FromDateTime(DateTime.Now) >= C.EndDate &&
            C.ContractStatus != ContractStatus.Terminated &&
            C.ContractStatus != ContractStatus.Cancelled &&
            C.ContractStatus != ContractStatus.Expired)
        {
            AddInclude(C => C.Employee!);
            AddInclude(C => C.Project!);
        }
    }
}
