using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.Entities.Tasks;
using status = Karim.Customer.HrApplication.Domain.Entities.Tasks.TaskStatus;
using Karim.Customer.HrApplication.Domain.Entities.Tickets;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance.BulkDtos;
using Karim.Customer.HrApplication.Shared.DTOs.Auth;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Contracts;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Karim.Customer.HrApplication.Shared.DTOs.Department.DepartmentToUploadBulkDtos;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using Karim.Customer.HrApplication.Shared.DTOs.Employees.BulkUploadDtos;
using Karim.Customer.HrApplication.Shared.DTOs.Projects;
using Karim.Customer.HrApplication.Shared.DTOs.Tasks;
using Karim.Customer.HrApplication.Shared.DTOs.Tickets;
using Mapster;
using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;

namespace Karim.Customer.HrApplication.Application.MapsterConfigurations
{
    internal class MapConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Department Section
            config.NewConfig<Department, DepartmentToReturnDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString())
                .Map(dest => dest.DepartmentPhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.DepartmentPhotoUrl!))
                .Map(dest => dest.ManagerName, src => src.Manager!.FullName)
                .Map(dest => dest.ManagerId, src => src.ManagerId)
                .Map(dest => dest.ManagerCode, src => src.Manager!.EmployeeCode);
            config.NewConfig<Department, SingleDepartmentToReturnDto>()
                .Map(dest => dest.DepatrmentType, src => src.DepatrmentType.ToString())
                .Map(dest => dest.ManagerName, src => src.Manager!.FullName)
                .Map(dest => dest.ManagerId, src => src.ManagerId)
                .Map(dest => dest.ManagerCode, src => src.Manager!.EmployeeCode)
                .Map(dest => dest.Employees, src => src.Employees)
                .Map(dest => dest.DepartmentPhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.DepartmentPhotoUrl!));
            config.NewConfig<DepartmentToAddDto, Department>()
                .Map(dest => dest.NormalizedName, src => src.DepartmentName.ToUpper())
                .Map(dest => dest.ManagerId, src => src.ManagerId)
                .Map(dest => dest.DepatrmentType, src => (DepartmentType)src.DepatrmentType);
            config.NewConfig<DepartmentToUpdateDto, Department>()
                .Map(dest => dest.NormalizedName, src => src.DepartmentName.ToUpper())
                .Map(dest => dest.ManagerId, src => src.ManagerId)
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
            //Fills
            config.NewConfig<Department, FillEntityDto<string>>()
                .Map(dest => dest.Name, src => src.DepartmentName)
                .Map(dest => dest.Code, src => src.DepartmentCode);


            //Employee Configs
            config.NewConfig<Employee, EmployeeToReturnDto>()
                .Map(dest => dest.WorkType, src => src.WorkType.ToString())
                .Map(dest => dest.EmployeeType, src => src.EmployeeType.ToString())
                .Map(dest => dest.EmployeeStatus, src => src.EmployeeStatus.ToString())
                .Map(dest => dest.Rank, src => src.Rank.ToString())
                .Map(dest => dest.Department, src => src.Department!.DepartmentName)
                .Map(dest => dest.DepartmentId, src => src.DepartmentId)
                .Map(dest => dest.DepartmentCode, src => src.Department!.DepartmentCode)
                .Map(dest => dest.PhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.PhotoUrl!));

            config.NewConfig<Employee, SpecificEmployeeToReturnDto>()
                .Map(dest => dest.PhotoUrl, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.PhotoUrl!))
                .Map(dest => dest.WorkType, src => src.WorkType.ToString())
                .Map(dest => dest.EmployeeType, src => src.EmployeeType.ToString())
                .Map(dest => dest.EmployeeStatus, src => src.EmployeeStatus.ToString())
                .Map(dest => dest.Rank, src => src.Rank.ToString())
                .Map(dest => dest.Department, src => src.Department!.DepartmentName)
                .Map(dest => dest.DepartmentId, src => src.DepartmentId)
                .Map(dest => dest.DepartmentCode, src => src.Department!.DepartmentCode);
   
            config.NewConfig<SingleEmployeeToAddDto, Employee>()
                .Map(dest => dest.WorkType, src => (WorkType)src.WorkType)
                .Map(dest => dest.FullNameNormalized, src => src.FullName.ToUpper())
                .Map(dest => dest.EmployeeType, src => (EmployeeType)src.EmployeeType)
                .Map(dest => dest.Rank, src => (EmployeeRank)src.EmployeeRank);

            config.NewConfig<SingleEmployeeToUpdateDto, Employee>()
                .Map(dest => dest.FullNameNormalized, src => src.FullName.ToUpper())
                .Map(dest => dest.WorkType, src => (WorkType)src.WorkType)
                .Map(dest => dest.EmployeeType, src => (EmployeeType)src.EmployeeType)
                .Map(dest => dest.Rank, src => (EmployeeRank)src.EmployeeRank);

            config.NewConfig<Employee, EmployeeInDepartmentDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.EmpCode, src => src.EmployeeCode)
                .Map(dest => dest.EmpName, src => src.FullName)
                .Map(dest => dest.Photo, src => MapContext.Current.GetService<FilesPathResolver>().Resolve(src.PhotoUrl!))
                .Map(dest => dest.Rank, src => src.Rank)
                .Map(dest => dest.Position, src => src.Position);

            config.NewConfig<BulkAddEmployeeDto, Employee>()
                .Map(dest => dest.FullNameNormalized, src => src.FullName!.ToUpper())
                .Map(dest => dest.WorkType, src => (WorkType)src.WorkType)
                .Map(dest => dest.EmployeeType, src => (EmployeeType)src.EmployeeType)
                .Map(dest => dest.Rank, src => (EmployeeRank)src.EmployeeRank);

            //Fills
            config.NewConfig<Employee, FillEntityDto<string>>()
                .Map(dest => dest.Code, src => src.EmployeeCode)
                .Map(dest => dest.Name, src => src.FullName);


            //Auth Section
            config.NewConfig<AppPrivilages, PrivilagesToReturnDto>();

            //Fingerprint
            config.NewConfig<Fingerprint, SpecificFingerprintToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.EmployeeName, src => src.Employee.FullName)
                .Map(dest => dest.CheckIn, src => src.CheckIn.ToString("hh:mm tt"))
                .Map(dest => dest.CheckOut, src => src.CheckOut.HasValue ? src.CheckOut.Value.ToString("hh:mm tt") : null);

            config.NewConfig<FingerprintToBeInsertDto, Fingerprint>();

            config.NewConfig<Fingerprint, FingerprintDetailsToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.EmployeeName, src => src.Employee.FullName)
                .Map(dest => dest.CheckIn, src => src.CheckIn.ToString("hh:mm tt"))
                .Map(dest => dest.CheckOut, src => src.CheckOut.HasValue ? src.CheckOut.Value.ToString("hh:mm tt") : null);

            config.NewConfig<Fingerprint, FingerprintToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.EmployeeName, src => src.Employee.FullName)
                .Map(dest => dest.EmpId, src => src.Employee.Id)
                .Map(dest => dest.Duration, src => src.DurationInHours)
                .Map(dest => dest.FingerprintId, src => src.Id)
                .Map(dest => dest.CheckIn, src => src.CheckIn.ToString("hh:mm tt"))
                .Map(dest => dest.CheckOut, src => src.CheckOut.HasValue ? src.CheckOut.Value.ToString("hh:mm tt") : null);

            config.NewConfig<FingerprintToAddDto, Fingerprint>()
                .Map(dest => dest.Status, src => (FingerprintStatus)src.Status)
                .Map(dest => dest.CheckInLong, src => src.Long)
                .Map(dest => dest.CheckInLat, src => src.Lat)
                .Map(dest => dest.CheckOutLong, src => src.Long)
                .Map(dest => dest.CheckOutLat, src => src.Lat);

            config.NewConfig<FingerprintToUpdateDto, Fingerprint>()
                .Map(dest => dest.CheckInLong, src => src.Long)
                .Map(dest => dest.CheckInLat, src => src.Lat)
                .Map(dest => dest.CheckOutLong, src => src.Long)
                .Map(dest => dest.CheckOutLat, src => src.Lat);



            config.NewConfig<AddCheckInBulkDto, Fingerprint>()
                .Map(dest => dest.Date, src => DateOnly.FromDateTime(DateTime.Now.Date))
                .Map(dest => dest.DurationInHours, src => src.CheckIn.HasValue && src.CheckOut.HasValue ? (src.CheckOut.Value - src.CheckIn.Value).TotalHours : 0)
                .Map(dest => dest.CheckInLong, src => 0)
                .Map(dest => dest.CheckInLat, src => 0)
                .Map(dest => dest.EmpId, src => src.EmpCode)
                .Map(dest => dest.Status, src =>
                                                !src.CheckIn.HasValue
                                                ? FingerprintStatus.InActive
                                                : src.CheckIn.Value > new TimeOnly(9, 0, 0)
                                                ? FingerprintStatus.Late
                                                : src.CheckOut.HasValue && (src.CheckOut.Value - src.CheckIn.Value).TotalHours < 8
                                                ? FingerprintStatus.Delay
                                                : src.CheckOut.HasValue && (src.CheckOut.Value - src.CheckIn.Value).TotalHours >= 8
                                                ? FingerprintStatus.InActive
                                                : FingerprintStatus.Active);

            //Requests
            config.NewConfig<RequestToAddDto, Requests>()
                .Map(dest => dest.Status, src => RequestStatus.Pending)
                .Map(dest => dest.EndDate, src => src.EndDate.HasValue == false ? src.StartDate : src.EndDate.Value)
                .Map(dest => dest.Type, src => (RequestType)src.Type);

            config.NewConfig<RequestToEditDto, Requests>()
                .Map(dest => dest.EndDate, src => src.EndDate.HasValue == false ? src.StartDate : src.EndDate.Value)
                .Map(dest => dest.Type, src => (RequestType)src.Type);

            config.NewConfig<Requests, RequestDetailsToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.Type, src => src.Type.ToString())
                .Map(dest => dest.EmployeeName, src => src.Employee.FullName);

            config.NewConfig<Requests, RequestToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.Type, src => src.Type.ToString())
                .Map(dest => dest.EmployeeName, src => src.Employee.FullName);

            //Projects
            config.NewConfig<ProjectToAddDto, Project>()
                .Map(dest => dest.ProjectType, src => (ProjectType)src.ProjectType)
                .Map(dest => dest.ProjectStatus, src => ProjectStatus.Draft)
                .Map(dest => dest.CoastCurrency, src => (Currancies)src.CoastCurrency);

            config.NewConfig<ProjectToUpdateDto, Project>()
                .Map(dest => dest.ProjectType, src => (ProjectType)src.ProjectType)
                .Map(dest => dest.CoastCurrency, src => (Currancies)src.CoastCurrency);

            config.NewConfig<Project, ProjectDetailsToReturnDto>()
                .Map(dest => dest.ProjectType, src => src.ProjectType.ToString())
                .Map(dest => dest.ProjectStatus, src => src.ProjectStatus.ToString())
                .Map(dest => dest.CoastCurrency, src => src.CoastCurrency.ToString())
                .Map(dest => dest.Department, src => src.Department!.DepartmentName);

            config.NewConfig<Project, ProjectToReturnDto>()
                .Map(dest => dest.ProjectType, src => src.ProjectType.ToString())
                .Map(dest => dest.ProjectStatus, src => src.ProjectStatus.ToString())
                .Map(dest => dest.CoastCurrency, src => src.CoastCurrency.ToString())
                .Map(dest => dest.Department, src => src.Department!.DepartmentName);

            config.NewConfig<Project, FillEntityDto<string>>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.ProjectName)
                .Map(dest => dest.Code, src => src.ProjectCode);

            //contracts
            config.NewConfig<EmployeeContractToAddDto, Contract>()
                .Map(dest => dest.EmployeeWorkType, src => (WorkType)src.EmployeeWorkType)
                .Map(dest => dest.CurrencyType, src => (Currancies)src.CurrencyType)
                .Map(dest => dest.ContractStatus, src => ContractStatus.Draft)
                .Map(dest => dest.ContractType, src => ContractType.Employee);

            config.NewConfig<ProjectContractToAddDto, Contract>()
                .Map(dest => dest.CurrencyType, src => (Currancies)src.CurrencyType)
                .Map(dest => dest.PaymentTerm, src => (PaymentTerm)src.PaymentTerm)
                .Map(dest => dest.ContractStatus, src => ContractStatus.Draft)
                .Map(dest => dest.ContractType, src => ContractType.Project);

            config.NewConfig<ProjectContractToUpdateDto, Contract>()
                .Map(dest => dest.CurrencyType, src => (Currancies)src.CurrencyType)
                .Map(dest => dest.PaymentTerm, src => (PaymentTerm)src.PaymentTerm);

            config.NewConfig<EmployeeContractToUpdateDto, Contract>()
                .Map(dest => dest.EmployeeWorkType, src => (WorkType)src.EmployeeWorkType)
                .Map(dest => dest.CurrencyType, src => (Currancies)src.CurrencyType);

            config.NewConfig<Contract, ProjectContractDetailsToReturnDto>()
                .Map(dest => dest.ContractDate, src => src.ContractDate.ToString())
                .Map(dest => dest.StartDate, src => src.StartDate.ToString())
                .Map(dest => dest.EndDate, src => src.EndDate.ToString())
                .Map(dest => dest.PaymentTerm, src => src.PaymentTerm.ToString())
                .Map(dest => dest.CurrencyType, src => src.CurrencyType.ToString())
                .Map(dest => dest.ProjectName, src => src.Project!.ProjectName)
                .Map(dest => dest.ProjectCode, src => src.Project!.ProjectCode)
                .Map(dest => dest.ContractType, src => src.ContractType.ToString())
                .Map(dest => dest.ContractStatus, src => src.ContractStatus.ToString());

            config.NewConfig<Contract, EmployeeContractDetailsToReturnDto>()
                .Map(dest => dest.ContractDate, src => src.ContractDate.ToString())
                .Map(dest => dest.StartDate, src => src.StartDate.ToString())
                .Map(dest => dest.EndDate, src => src.EndDate.ToString())
                .Map(dest => dest.CurrencyType, src => src.CurrencyType.ToString())
                .Map(dest => dest.EmployeeWorkType, src => src.EmployeeWorkType.ToString())
                .Map(dest => dest.EmployeeName, src => src.Employee!.FullName)
                .Map(dest => dest.EmployeeCode, src => src.Employee!.EmployeeCode)
                .Map(dest => dest.ContractType, src => src.ContractType.ToString())
                .Map(dest => dest.ContractStatus, src => src.ContractStatus.ToString());

            config.NewConfig<Contract, ContractToReturnDto>()
                .Map(dest => dest.ContractType, src => src.ContractType.ToString())
                .Map(dest => dest.ContractStatus, src => src.ContractStatus.ToString())
                .Map(dest => dest.ContractDate, src => src.ContractDate.ToString())
                .Map(dest => dest.StartDate, src => src.StartDate.ToString())
                .Map(dest => dest.EndDate, src => src.EndDate.ToString())
                .Map(dest => dest.PaymentTerm, src => src.PaymentTerm.ToString())
                .Map(dest => dest.CurrencyType, src => src.CurrencyType.ToString())
                .Map(dest => dest.EmployeeWorkType, src => src.EmployeeWorkType.ToString())
                .Map(dest => dest.EmployeeName, src => src.Employee!.FullName)
                .Map(dest => dest.EmployeeCode, src => src.Employee!.EmployeeCode)
                .Map(dest => dest.ProjectName, src => src.Project!.ProjectName)
                .Map(dest => dest.ProjectCode, src => src.Project!.ProjectCode);

            //Tickets
            config.NewConfig<TicketToAddDto, Ticket>()
                .Map(dest => dest.NormalizedName, src => src.Name.ToUpper())
                .Map(dest => dest.Status, src => TicketStatus.Opened)
                .Map(dest => dest.IsArchive, src => false);

            config.NewConfig<TicketToUpdateDto, Ticket>()
                .Map(dest => dest.NormalizedName, src => src.Name.ToUpper());

            config.NewConfig<Ticket, TicketDetailsToReturnDto>()
                .Map(dest => dest.StatusId, src => src.Status)
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.ProjectCode, src => src.Project.ProjectCode)
                .Map(dest => dest.ProjectName, src => src.Project.ProjectName);

            config.NewConfig<Ticket, TicketToReturnDto>()
                .Map(dest => dest.StatusId, src => src.Status)
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.ProjectCode, src => src.Project.ProjectCode)
                .Map(dest => dest.ProjectName, src => src.Project.ProjectName);

            //Tasks
            config.NewConfig<TaskToAddDto, Tasks>()
                .Map(dest => dest.TaskHours, src => src.AssignedHours)
                .Map(dest => dest.RemainingHours, src => src.AssignedHours)
                .Map(dest => dest.Status, src => status.New)
                .Map(dest => dest.Type, src => (TaskType)src.Type);

            config.NewConfig<TaskToUpdateDto, Tasks>()
                .Map(dest => dest.TaskHours, src => src.AssignedHours);

            config.NewConfig<Tasks, TaskDetailsToReturnDto>()
                 .Map(dest => dest.Status, src => src.Status.ToString())
                 .Map(dest => dest.StatusId, src => (status)src.Status)
                .Map(dest => dest.Type, src => src.Type.ToString())
                .Map(dest => dest.TypeId, src => (TaskType)src.Type)
                .Map(dest => dest.ProjectCode, src => src.Project!.ProjectCode)
                .Map(dest => dest.ProjectName, src => src.Project!.ProjectName)
                .Map(dest => dest.TicketCode, src => src.Ticket!.TicketCode)
                .Map(dest => dest.TicketName, src => src.Ticket!.Name)
                .Map(dest => dest.EmployeeCode, src => src.Employee!.EmployeeCode)
                .Map(dest => dest.EmployeeName, src => src.Employee!.FullName);

            config.NewConfig<Tasks, TaskToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.StatusId, src => (status)src.Status)
                .Map(dest => dest.Type, src => src.Type.ToString())
                .Map(dest => dest.TypeId, src => (TaskType)src.Type)
                .Map(dest => dest.ProjectCode, src => src.Project!.ProjectCode)
                .Map(dest => dest.ProjectName, src => src.Project!.ProjectName)
                .Map(dest => dest.TicketCode, src => src.Ticket!.TicketCode)
                .Map(dest => dest.TicketName, src => src.Ticket!.Name)
                .Map(dest => dest.EmployeeCode, src => src.Employee!.EmployeeCode)
                .Map(dest => dest.EmployeeName, src => src.Employee!.FullName);

            //Payrolls
            config.NewConfig<PayrollBonus, PayrollBonusToReturnDto>();
            config.NewConfig<PayrollAllowance, PayrollAllowanceToReturnDto>();
            config.NewConfig<PayrollPenalty, PayrollPenaltyToReturnDto>();

            config.NewConfig<Payslip, PayslipToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.StatusId, src => (status)src.Status)
                .Map(dest => dest.PaymentWay, src => src.PaymentWay.HasValue ? src.PaymentWay.Value.ToString() : null)
                .Map(dest => dest.PaymentWayId, src => src.PaymentWay.HasValue ? (PayrollPaymentWay)src.PaymentWay.Value : 0)
                .Map(dest => dest.EmployeeName, src => src.Employee.FullName)
                .Map(dest => dest.EmployeeCode, src => src.Employee.EmployeeCode);

            config.NewConfig<Payslip, PayslipDetailsToReturnDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.StatusId, src => (status)src.Status)
                .Map(dest => dest.PaymentWay, src => src.PaymentWay.HasValue ? src.PaymentWay.Value.ToString() : null)
                .Map(dest => dest.PaymentWayId, src => src.PaymentWay.HasValue ? (PayrollPaymentWay)src.PaymentWay.Value : 0)
                .Map(dest => dest.EmployeeName, src => src.Employee.FullName)
                .Map(dest => dest.EmployeeCode, src => src.Employee.EmployeeCode);

            config.NewConfig<PenaltyToAddDto, PayrollPenalty>();

            config.NewConfig<PenaltyToEditDto, PayrollPenalty>();

            config.NewConfig<BonusToAddDto, PayrollBonus>();

            config.NewConfig<BonusToEditDto, PayrollBonus>();

            config.NewConfig<AllowanceToAddDto, PayrollAllowance>();

            config.NewConfig<AllowanceToEditDto, PayrollAllowance>();

            config.NewConfig<PayslipToAddDto, Payslip>()
                .Map(dest => dest.Status, src => PayrollStatus.Pending)
                .Map(dest => dest.PaymentWay, src => (PayrollPaymentWay?)null)
                .Map(dest => dest.EmployeeType, src => EmployeeType.Freelance);

            config.NewConfig<AutoPayslipToAddDto, Payslip>()
                .Map(dest => dest.Status, src => PayrollStatus.Pending)
                .Map(dest => dest.EmployeeType, src => (EmployeeType)src.EmployeeType)
                .Map(dest => dest.PaymentWay, src => (PayrollPaymentWay?)null);

            config.NewConfig<PayslipToEditDto, Payslip>()
                .Map(dest => dest.Status, src => (PayrollStatus)src.Status)
                .Map(dest => dest.PaymentWay, src => src.PaymentWay.HasValue ? (PayrollPaymentWay)src.PaymentWay.Value : (PayrollPaymentWay?)null);

        }
    }
}
