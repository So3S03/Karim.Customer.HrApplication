using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Karim.Customer.HrApplication.Shared.DTOs.Department.DepartmentToUploadBulkDtos;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
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


            //Employee Configs
            config.NewConfig<Employee, EmployeeToReturnDto>()
                .Map(dest => dest.WorkType, src => src.WorkType.ToString())
                .Map(dest => dest.EmployeeType, src => src.EmployeeType.ToString())
                .Map(dest => dest.EmployeeStatus, src => src.EmployeeStatus.ToString())
                .Map(dest => dest.Department, src => src.Department.DepartmentName)
                .Map(dest => dest.DepartmentId, src => src.DepartmentId)
                .Map(dest => dest.DepartmentCode, src => src.Department.DepartmentCode)
                .Map(dest => dest.PhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.PhotoUrl!));

            config.NewConfig<Employee, SpecificEmployeeToReturnDto>()
                .Map(dest => dest.PhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.PhotoUrl!))
                .Map(dest => dest.WorkType, src => src.WorkType.ToString())
                .Map(dest => dest.EmployeeType, src => src.EmployeeType.ToString())
                .Map(dest => dest.EmployeeStatus, src => src.EmployeeStatus.ToString())
                .Map(dest => dest.Department, src => src.Department.DepartmentName)
                .Map(dest => dest.DepartmentId, src => src.DepartmentId)
                .Map(dest => dest.DepartmentCode, src => src.Department.DepartmentCode);
        }
    }
}
