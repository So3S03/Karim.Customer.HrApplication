using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Projects;
using Karim.Customer.HrApplication.Application.Specifications.Projects;
using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Projects;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;

namespace Karim.Customer.HrApplication.Application.Services.Projects
{
    internal class ProjectServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProjectServices
    {
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
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Project, string>();
            //Forming Spec
            var Spec = new ProjectByIdSpecification(project.Id);
            //Get Project
            var ExistingProject = await Repo.GetByIdAsyncWithNoTracking(Spec);
            //Check On Project
            if (ExistingProject is null) throw new NotFoundException("Project Not Found");
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
    }
}
