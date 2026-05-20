using task = Karim.Customer.HrApplication.Domain.Entities.Tasks.Tasks;

namespace Karim.Customer.HrApplication.Application.Specifications.Task
{
    internal class LastTaskSpecification : BaseSpecifications<task, string>
    {
        public LastTaskSpecification()
        {
            SetOrderByDesc(T => T.Code);
        }
    }
}
