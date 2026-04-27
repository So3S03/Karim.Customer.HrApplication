using Karim.Customer.HrApplication.Application._Common.EnumConverter;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Projects;
using Karim.Customer.HrApplication.Application.Specifications.Department;
using Karim.Customer.HrApplication.Application.Specifications.Projects;
using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Projects;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Karim.Customer.HrApplication.Application.Services.Projects
{
    internal class ProjectServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProjectServices
    {
        private const string codePattern = @"^PROJ\d{3,}$";
        public async Task<ActionStatusDto> CreateProject(ProjectToAddDto? project)
        {
            //Check On Data
            if (project is null) throw new BadRequestException("Must Provide Data To Add A Project");
            //Check On Specific Data
            _ = project switch
            {
                { ProjectCode: null or "" } => throw new BadRequestException("Project Code Must Be Provided"),
                { ProjectName: null or ""} => throw new BadRequestException("Project Name Must Be Provided"),
                { ProjectType: var t} when !Enum.IsDefined(typeof(ProjectType), t) => throw new BadRequestException("Invalid Project Type"),
                { CoastCurrency: var c} when !Enum.IsDefined(typeof(Currancies), c) => throw new BadRequestException("Invalid Currency Type"),
                _ => project
            };
            //Check On Code Pattern
            if (!Regex.IsMatch(project.ProjectCode, codePattern)) throw new BadRequestException("Code Pattern Is Not Correct!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Spec
            var Spec = new ProjectByCodeSpecification(project.ProjectCode);
            //Get Project
            var ExistingProject = await Repo.GetByIdAsyncWithNoTracking(Spec);
            //Check If Exist
            if (ExistingProject is not null) throw new ConflictException("Project Code Exist!");
            //Mapping Project
            var mappedProject = _mapper.Map<Project>(project);
            //Add Project
            await Repo.AddAsync(mappedProject);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Form Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = $"Project Created Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> DeleteProject(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Creaye Spec
            var Spec = new ProjectByIdSpecification(Id);
            //Get Project
            var ExistingProject = await Repo.GetByIdAsync(Spec);
            //Check If Exist
            if (ExistingProject is null) throw new NotFoundException("Project Not Exist!");
            //Check If The Project is InProgress
            if (ExistingProject.ProjectStatus == ProjectStatus.InProgress || ExistingProject.ProjectStatus == ProjectStatus.Active) throw new BadRequestException("You Can not Delete An InProgress Or Active Project!");
            //Check If Project Has Dependancies Like Department Or Tasks
            if (ExistingProject.DepartmentId is not null) throw new BadRequestException("Can't Hold Project Already Assigned To An Department!");
            //Delete Project
            Repo.Delete(ExistingProject);
            //Compelete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Project Deleted Successfully!"
            };
            return Obj;
        }
        public async Task<DataWithPagination<ICollection<ProjectToReturnDto>>> GetAllProjects(ProjectParameters? parameters)
        {
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Specification
            var Spec = new ProjectsListSpecifiaction(parameters);
            //Get All Projects
            var AllProjects = await Repo.GetAllAsync(Spec);
            //Get Count
            var Count = await Repo.GetDataCountAsync(Spec);//it may need adjusting
            //Calc Pages Number
            var PagesCount = Math.Ceiling((decimal)(Count / parameters!.PageSize));
            //Mapping Data
            var mappedData = _mapper.Map<ICollection<ProjectToReturnDto>>(AllProjects);
            //Create Object
            var Obj = new DataWithPagination<ICollection<ProjectToReturnDto>>(
                pageNum: parameters.PageNum,
                nextPage: parameters.PageNum > PagesCount ? PagesCount : parameters.PageNum,
                pageSize: parameters.PageSize,
                totalRecords: Count,
                mappedData
                );
            return Obj;
        }
        public async Task<ProjectDetailsToReturnDto> GetProjectById(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Creaye Spec
            var Spec = new ProjectByIdSpecification(Id);
            //Get Project
            var ExistingProject = await Repo.GetByIdAsync(Spec);
            //Check If Exist
            if (ExistingProject is null) throw new NotFoundException("Project Not Exist!");
            //Mapping Projecet
            var mappedProject = _mapper.Map<ProjectDetailsToReturnDto>(ExistingProject);
            //return
            return mappedProject;
        }
        public async Task<ActionStatusDto> UpdateProject(ProjectToUpdateDto? project)
        {
            //Check On project
            if (project is null) throw new BadRequestException("Must Provide Data");
            //Check On Inernal Data
            _ = project switch
            {
                { Id: null or ""} => throw new BadRequestException("Id Is Invalid!"),
                { ProjectCode: null or "" } => throw new BadRequestException("Project Code Must Be Provided"),
                { ProjectName: null or "" } => throw new BadRequestException("Project Name Must Be Provided"),
                { ProjectType: var t } when !Enum.IsDefined(typeof(ProjectType), t) => throw new BadRequestException("Invalid Project Type"),
                { CoastCurrency: var c } when !Enum.IsDefined(typeof(Currancies), c) => throw new BadRequestException("Invalid Currency Type"),
                _ => project
            };
            //Check If Code is match pattern
            if (!Regex.IsMatch(project.ProjectCode, codePattern)) throw new BadRequestException("Code Not Mathc The Pattern PROJ001");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Forming Spec
            var Spec = new ProjectByIdSpecification(project.Id);
            //Get Project
            var ExistingProject = await Repo.GetByIdAsyncWithNoTracking(Spec);
            //Check On Project
            if (ExistingProject is null) throw new NotFoundException("Project Not Found");
            //Check If Codes Matches
            if (ExistingProject.ProjectCode != project.ProjectCode) throw new BadRequestException("Incoming Code Not Match The Targted Project For Update!");
            //Mapping Project
            var mappedProject = _mapper.Map(project, ExistingProject);
            //Update 
            Repo.Update(mappedProject);
            //Compelete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Project Updated Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> ActivateProject(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Project Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Spec
            var Spec = new ProjectByIdSpecification(Id);
            //Get Project
            var Project = await Repo.GetByIdAsync(Spec);
            //Check On Project
            if (Project is null) throw new NotFoundException(Id, "Project");
            //Check if Project is only Draft
            if (Project.ProjectStatus != ProjectStatus.Draft) throw new BadRequestException("Project Status Must Be New To Activate It!");
            //Check If Has Contract
            if (Project.ContractId is null) throw new BadRequestException("Can't Activate Project That Has No Contract");
            //Edit Status
            Project.ProjectStatus = ProjectStatus.Active;
            Project.ActivatedAt = DateTime.UtcNow;
            //Update Project
            Repo.Update(Project);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Project Activated Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> CancelProject(ProjectToCancelDto? cancelDto)
        {
            //Check On Data
            if (cancelDto is null) throw new BadRequestException("Invalid Data!");
            //Check on Internal data
            _ = cancelDto switch
            {
                { ProjectId: null or "" } => throw new BadRequestException("Invalid Project Id!"),
                { CancelationReason: null or "" } => throw new BadRequestException("Must Provide Reason For Project Cancelation!"),
                _ => cancelDto
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Spec
            var Spec = new ProjectByIdSpecification(cancelDto.ProjectId);
            //Get Project
            var Project = await Repo.GetByIdAsync(Spec);
            //Chcek On Project
            if (Project is null) throw new NotFoundException(cancelDto.ProjectId, "Project");
            //Check If Already Canceled Or Completed
            if (Project.ProjectStatus == ProjectStatus.Cancelled) throw new ConflictException("Can't Cancel An Alread Canceld Project!");
            if (Project.ProjectStatus == ProjectStatus.Completed) throw new BadRequestException("Can't Cancel An Completed Project!");
            //Update Columns
            Project.ProjectStatus = ProjectStatus.Cancelled;
            Project.CancelationReason = cancelDto.CancelationReason;
            Project.CanceledAt = DateTime.UtcNow;
            //Update
            Repo.Update(Project);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Objecct
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Project Canceled Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> HoldProject(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invlalid Id");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Spec
            var Spec = new ProjectByIdSpecification(Id);
            //Get Project
            var ExistingProject = await Repo.GetByIdAsync(Spec);
            //Check On it
            if (ExistingProject is null) throw new NotFoundException(Id, "Project");
            //Check if project already on hold
            if (ExistingProject.ProjectStatus == ProjectStatus.OnHold) throw new ConflictException("Project Already On Hold!");
            //Check If Project Has Dependancies Like Department Or Tasks
            if (ExistingProject.DepartmentId is not null) throw new BadRequestException("Can't Hold Project Already Assigned To An Department!");
            //if (ExistingProject.Tasks is not null) throw new BadRequestException("Can't Hold Project Already Has On Going Tasks!");
            //Update Project
            ExistingProject.ProjectStatus = ProjectStatus.OnHold;
            //Update
            Repo.Update(ExistingProject);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Create Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Project Holded Successfully!"
            };
            return Obj;
        }
        public async Task<MaxCodeResult> CreateMaxProjectCode()
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Spec
            var Spec = new LastProjectCodeSpecification();
            //Get Projects Count
            var Proj = await Repo.GetByIdAsync(Spec);
            //Create Base Code
            string Code = "PROJ";
            //Create Object
            var MaxCode = new MaxCodeResult();
            //Checck If It's the first project
            if (Proj is null)
            {
                Code = $"{Code}001";
                MaxCode.MaxCode = Code;
                return MaxCode;
            }
            //Extract Code
            var ExtractedCode = Proj!.ProjectCode;
            //Extract Numaric Part
            int.TryParse(ExtractedCode.Split("J")[1], out var NumericPart);
            //Compine BaseCode With Code Number
            Code = $"{Code}{(NumericPart + 1).ToString().PadLeft(3, '0')}";
            MaxCode.MaxCode = Code;
            return MaxCode;
        }
        public async Task<ActionStatusDto> AssignProjectToDepartment(ProjectToAssignDto? data)
        {
            //Check On Id
            if (data is null) throw new BadRequestException("Invalid data!");
            //Chcek On Internal Data
            _ = data switch
            {
                { DepartmentId: null or "" } => throw new BadRequestException("Must Assign Department For The Project!"),
                { Id: null or "" } => throw new BadRequestException("Invalid Project Id!"),
                _ => data
            };
            //Create Repo 
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Spec
            var Spec = new ProjectByIdSpecification(data.Id);
            //Get Project
            var Project = await Repo.GetByIdAsync(Spec);
            //Check On Proj
            if (Project is null) throw new NotFoundException(data.Id, "Project");
            //Check If Not Active
            if (Project.ProjectStatus != ProjectStatus.Active) throw new BadRequestException("Project Must Be Activated Before Any Assigning!");
            //Update Columns
            Project.DepartmentId = data.DepartmentId;
            Project.ProjectStatus = ProjectStatus.InProgress;
            //Update
            Repo.Update(Project);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Project Assigned Successfully!"
            };
            return Obj;
        }

        public ICollection<EnumDto> GetAllCurrencies()
        {
            //create list
            var list = EnumsConvertion.CreateEnumLists<CurrencyLockUp>().OrderBy(x => x.DisplayedName).ToList();
            return list;
        }

        public ICollection<EnumDto> GetAllProjectsTypes()
        {
            //create list
            var list = EnumsConvertion.CreateEnumLists<ProjectTypesLockUp>();
            return list;
        }

        public ICollection<EnumDto> GetAllProjectsStatus()
        {
            //create list
            var list = EnumsConvertion.CreateEnumLists<ProjectStatusLockUp>();
            return list;
        }

        public async Task<ICollection<FillEntityDto<string>>> FillProjects()
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Create Spec
            var Spec = new AllActivatedAndInProgressProjectsList();
            //Get All Projects
            var List = await Repo.GetAllAsync(Spec);
            //Mapping Data
            var MappedData = _mapper.Map<ICollection<FillEntityDto<string>>>(List);
            //return data
            return MappedData;
        }
    }
}
