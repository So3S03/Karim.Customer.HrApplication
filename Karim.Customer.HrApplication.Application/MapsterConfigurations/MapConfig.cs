using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Karim.Customer.HrApplication.Shared.DTOs.Department.DepartmentToUploadBulkDtos;
using Mapster;

namespace Karim.Customer.HrApplication.Application.MapsterConfigurations
{
    internal class MapConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Department Section
            config.NewConfig<Department, DepartmentToReturnDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString())
                .Map(dest => dest.DepartmentPhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.DepartmentPhotoUrl!));
            config.NewConfig<Department, SingleDepartmentToReturnDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString())
                .Map(dest => dest.DepartmentPhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.DepartmentPhotoUrl!));
            config.NewConfig<DepartmentToAddDto, Department>()
                .Map(dest => dest.NormalizedName, src => src.DepartmentName.ToUpper())
                .Map(dest => dest.DepatrmentType, src => (DepartmentType)src.DepatrmentType);
            config.NewConfig<DepartmentToUpdateDto, Department>()
                .Map(dest => dest.NormalizedName, src => src.DepartmentName.ToUpper())
                .Map(dest => dest.DepatrmentType, src => (DepartmentType)src.DepatrmentType);
            config.NewConfig<Department, DepartmentNameUploadBulkDto>();
            config.NewConfig<Department, DepartmentDescriptionUploadBulkDto>();
            config.NewConfig<Department, DepatrmentTypeUploadBulkDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString());
            config.NewConfig<Department, DepartmentActualCreationDateUploadBulkDto>();
            config.NewConfig<Department, DepartmentBudgetForSalariesUploadBulkDto>();
            config.NewConfig<Department, DepartmentBudgetForToolsUploadBulkDto>();
            config.NewConfig<Department, DepartmentBudgetForTraineesUploadBulkDto>();
            config.NewConfig<Department, DepartmentBudgetOtherUploadBulkDto>();
            config.NewConfig<Department, DepartmentTotalDepartmentBudgetUploadBulkDto>();
        }
    }
}
