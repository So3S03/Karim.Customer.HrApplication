using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Application.Specifications.Projects
{
    internal class ProjectByCodeSpecification : BaseSpecifications<Project, string>
    {
        public ProjectByCodeSpecification(string ProjectCode) : base (P => P.ProjectCode == ProjectCode)
        {
            
        }
    }
}
