using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Application.Specifications.Projects
{
    internal class ProjectByIdSpecification: BaseSpecifications<Project, string>
    {
        public ProjectByIdSpecification(string? ProjId): base(P => P.Id == ProjId)
        {
            AddInclude(P => P.Department!);
            AddInclude(P => P.Contract!);
        }
    }
}
