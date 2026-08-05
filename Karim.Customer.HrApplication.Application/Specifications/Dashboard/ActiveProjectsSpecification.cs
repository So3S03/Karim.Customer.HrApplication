using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class ActiveProjectsSpecification() : BaseSpecifications<Project, string>(Project =>
    Project.ProjectStatus == ProjectStatus.Active || Project.ProjectStatus == ProjectStatus.InProgress)
    {
    }
}
