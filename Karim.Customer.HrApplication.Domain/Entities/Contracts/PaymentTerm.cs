using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Domain.Entities.Contracts
{
    public enum PaymentTerm
    {
        UponCompletion = 1,
        MonthlyInstallments = 2,
        MilestoneBased = 3,
        HalfUpfrontHalfOnDelivery = 4,
        Net30 = 5
    }
}
