using Karim.Customer.HrApplication.Domain.Entities.Contracts;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class ExpiredContractsSpecifications(ContractType type) : BaseSpecifications<Contract, string>(
        C => C.ContractType == type && CurrentDate >= C.EndDate)
    {
        private static DateOnly CurrentDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }
}
