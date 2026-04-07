using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Shared.DTOs.Projects;

namespace Karim.Customer.HrApplication.Application.Specifications.Projects
{
    internal class ProjectsListSpecifiaction : BaseSpecifications<Project, string>
    {
        public ProjectsListSpecifiaction(ProjectParameters? parameters): base()
        {
            AddInclude(P => P.Department!);
            Pagination(parameters!.PageNum, parameters.PageSize);
        }
    }
}
