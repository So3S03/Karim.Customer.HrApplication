using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Mapster;

namespace Karim.Customer.HrApplication.Application.MapsterConfigurations
{
    internal class MapConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Department, DepartmentToReturnDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString());
        }
    }
}
