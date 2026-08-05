using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Application.Specifications.Projects
{
    internal class LastProjectCodeSpecification : BaseSpecifications<Project, string>
    {
        public LastProjectCodeSpecification()
        {
            SetOrderByDesc(P => P.ProjectCode);
        }
    }
}
