using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Mapster;

namespace Karim.Customer.HrApplication.Application.MapsterConfigurations
{
    internal class MapConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Department Section
            config.NewConfig<Department, DepartmentToReturnDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString());
            config.NewConfig<Department, SingleDepartmentToReturnDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString());
            config.NewConfig<DepartmentToAddDto, Department>()
                .Map(dest => dest.NormalizedName, src => src.DepartmentName.ToUpper())
                .Map(dest => dest.DepatrmentType, src => (DepartmentType)src.DepatrmentType);
        }
    }
}
