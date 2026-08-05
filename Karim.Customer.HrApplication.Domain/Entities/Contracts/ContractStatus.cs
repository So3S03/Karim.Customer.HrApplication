using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Domain.Entities.Contracts
{
    public enum ContractStatus
    {
        Draft = 1,
        Active = 2,
        Expired = 3,
        Terminated = 4,
        Cancelled = 5
    }
}
