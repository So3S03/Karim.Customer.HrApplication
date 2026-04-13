using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Application.Specifications.Projects
{
    internal class AllActivatedAndInProgressProjectsList : BaseSpecifications<Project, string>
    {
        public AllActivatedAndInProgressProjectsList(): base(P => P.ProjectStatus == ProjectStatus.Active ||  P.ProjectStatus == ProjectStatus.InProgress)
        {
            
        }
    }
}
