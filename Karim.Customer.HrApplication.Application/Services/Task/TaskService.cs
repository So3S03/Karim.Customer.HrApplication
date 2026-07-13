using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Projects;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Task;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Tickets;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using Karim.Customer.HrApplication.Application.Specifications.Projects;
using Karim.Customer.HrApplication.Application.Specifications.Task;
using Karim.Customer.HrApplication.Application.Specifications.Tasks;
using Karim.Customer.HrApplication.Application.Specifications.Tickets;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.Entities.Tasks;
using Karim.Customer.HrApplication.Domain.Entities.Tickets;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Tasks;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using System.Text.RegularExpressions;
using ticket = Karim.Customer.HrApplication.Domain.Entities.Tickets.Ticket;

namespace Karim.Customer.HrApplication.Application.Services.Task
{
    internal class TaskService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITaskService
    {
        private const string codePattern = @"^TSK\d{3,}$";
        public async Task<ActionStatusDto> AddTask(TaskToAddDto? task)
        {
            //Check On Data
            if (task is null) throw new BadRequestException("Invalid Data!");
            //Check On Specific Data
            _ = task switch
            {
                { Code: null or "" } => throw new BadRequestException("Ivalid Code"),
                { Code: var code } when !Regex.IsMatch(code, codePattern) => throw new BadRequestException("Ivalid Code"),
                { Name: null or "" } => throw new BadRequestException("Must Provide Name"),
                { EmployeeId: null or "" } => throw new BadRequestException("Must Provide Employee"),
                { AssignedHours: <= 0 } => throw new BadRequestException("Invalid Task Hours"),
                { Type: var type} when !Enum.IsDefined(typeof(TaskType), type) => throw new BadRequestException("Invalid Task Type"),
                _ => task
            };
            //Check On Project Id && Ticket Id
            if(string.IsNullOrEmpty(task.ProjectId) && string.IsNullOrEmpty(task.TicketId)) throw new BadRequestException("Must Provide Project Or Ticket To Add Task!");
            //Check If Both Have Values
            else if(string.IsNullOrEmpty(task.ProjectId) == false && string.IsNullOrEmpty(task.TicketId) == false) throw new BadRequestException("Cannot Add Task On Both Project And Ticket In The Same Time!");
            //Check If Employee Exist
            //Create Emp Repo
            var EmpRepo = _unitOfWork.GenerateRepository<Domain.Entities.Employee.Employee, string>();
            //Create Emp Spec
            var EmpSpec = new EmployeeByIdSepecification(task.EmployeeId);
            //Get Employee
            var Employee = await EmpRepo.GetByIdAsync(EmpSpec);
            //Check On Employee
            if(Employee is null) throw new NotFoundException("Employee Not Exist!");
            //Check That Employee Not Terminated
            if(Employee.EmployeeStatus.HasValue && (Employee.EmployeeStatus.Value == Domain.Entities.Employee.EmployeeStatus.Terminated || Employee.EmployeeStatus.Value == Domain.Entities.Employee.EmployeeStatus.Resigned))
                throw new ConflictException("Cannot Add Task To Terminated / Resigned Employee!");
            //Check On The Time Amount
            //Project
            if ((TaskType)task.Type == TaskType.Project)
            {
                if(string.IsNullOrEmpty(task.ProjectId)) throw new BadRequestException("Must Provid Project!");
                //Create Repo
                var ProjRepo = _unitOfWork.GenerateRepository<Project, string>();
                //Create Spec
                var ProjSpec = new ProjectByIdSpecification(task.ProjectId);
                //Get Proje t
                var project = await ProjRepo.GetByIdAsync(ProjSpec);
                //Check On It
                if (project is null) throw new NotFoundException("Project Not Exist!");
                //Check On Project Status
                switch(project.ProjectStatus)
                {
                    case ProjectStatus.Draft:
                        throw new ConflictException("Can't Add Task To Draft Project, Activate Project First!");
                    case ProjectStatus.Cancelled:
                        throw new ConflictException("Can't Add Task To Canceled Project!");
                    case ProjectStatus.OnHold:
                        throw new ConflictException("Can't Add Task To On-Hold Project!");
                    case ProjectStatus.Completed:
                        throw new ConflictException("Can't Add Task To Completed Project!");
                }
                //Check If Employee Exist In The Same Department As The Project
                if(Employee.DepartmentId != project.DepartmentId) throw new ConflictException("Employee Is Not In The Department The Project Assigned To!");
                //Check If The Project Hrs < Task Hrs
                if (task.AssignedHours > project.HoursAmount) throw new ConflictException("Task Hours Exceeded Project Hours, Must Be Equal Or Less Than Project Hours!");
                //Update Project Hours
                project.HoursAmount = project.HoursAmount - task.AssignedHours;
                //Update Project Status If Needed
                if(project.ProjectStatus == ProjectStatus.Draft) project.ProjectStatus = ProjectStatus.InProgress;
                //Update
                ProjRepo.Update(project);
            }
            //Ticket
            else if((TaskType)task.Type == TaskType.Ticket)
            {
                //Check If There Is Project | Ticket Exist
                if (string.IsNullOrEmpty(task.TicketId)) throw new BadRequestException("Must Provid Ticket!");
                //Create Repo
                var TickRepo = _unitOfWork.GenerateRepository<ticket, string>();
                //Create Spec
                var TickSpec = new TicketByIdSpecification(task.TicketId);
                //Get Proje t
                var ticket = await TickRepo.GetByIdAsync(TickSpec);
                //Check On It
                if (ticket is null) throw new NotFoundException("Ticket Not Exist!");
                //Check On Ticket Status
                if(ticket.Status == TicketStatus.Closed) throw new ConflictException("Can't Add Task To Closed Ticket!");
                //Check If Ticket Archived
                if(ticket.IsArchive == true) throw new ConflictException("Can't Add Task To Archived Ticket!");
                //Check On Ticket Period
                var today = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                //Check If End Date Passed Todays Date
                if(ticket.EndDate < today) throw new ConflictException($"Ticket Ended On {ticket.EndDate.ToString()}, Edit The Ticket Period First!");
                //Check If Task Period Exist In The Ticket Period
                if(task.StartDate < ticket.StartDate || task.EndDate > ticket.EndDate) throw new ConflictException($"Task Period {task.StartDate} - {task.EndDate} Is Not Within Ticket Period {ticket.StartDate} - {ticket.EndDate}!");
                //Check If The Project Hrs < Task Hrs
                if (task.AssignedHours > ticket.HoursNumber) throw new ConflictException("Task Hours Exceeded Ticket Hours, Must Be Equal Or Less Than Ticket Hours!");
                //Update Ticket Hours
                ticket.HoursNumber = ticket.HoursNumber - task.AssignedHours;
                //Update Ticket Status If Needed
                if (ticket.Status == TicketStatus.Opened) ticket.Status = TicketStatus.InProgres;
                //Update
                TickRepo.Update(ticket);
            }
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Creating MappedData
            var mappedData = _mapper.Map<Tasks>(task);
            //Add Task
            await Repo.AddAsync(mappedData);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Create Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task Added Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> ArchiveTask(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Create Spec
            var Spec = new TaskByIdSpecificatiion(Id);
            //Get Task
            var Task = await Repo.GetByIdAsync(Spec);
            //Check On Task
            if (Task is null) throw new NotFoundException("Task Not Exist!");
            //Check On Task Status
            if (Task.Status == Domain.Entities.Tasks.TaskStatus.InProgress) throw new ConflictException("Can't Archive InProgress Task!");
            //Check If Already Archived
            if (Task.isArchived) throw new ConflictException("Task Already Archived");
            //Update isArchived
            Task.isArchived = true;
            //Update
            Repo.Update(Task);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Form Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task Archived Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> CloseTask(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Create Spec
            var Spec = new TaskByIdSpecificatiion(Id);
            //Get Task
            var Task = await Repo.GetByIdAsync(Spec);
            //Check On Task
            if (Task is null) throw new NotFoundException("Task Not Exist!");
            //Check If Already Closed
            if (Task.Status == Domain.Entities.Tasks.TaskStatus.Closed) throw new ConflictException("Task Already Cloased!");
            //Close Task
            Task.Status = Domain.Entities.Tasks.TaskStatus.Closed;
            //Update
            Repo.Update(Task);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Form Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task Closed Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> DeleteTask(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Create Spec
            var Spec = new TaskByIdSpecificatiion(Id);
            //Get Task
            var Task = await Repo.GetByIdAsync(Spec);
            //Check On Task
            if (Task is null) throw new NotFoundException("Task Not Exist!");
            //Check On Task Status
            if(Task.Status == Domain.Entities.Tasks.TaskStatus.InProgress) throw new ConflictException("Can't Delete InProgress Task!");
            //Delete Task
            Repo.Delete(Task);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Form Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task Deleted Successfully!"
            };
            return Obj;
        }
        public async Task<MaxCodeResult> GenerateMaxCode()
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Create Spec
            var Spec = new LastTaskSpecification();
            //GetLastTask
            var LastTask = await Repo.GetByIdAsync(Spec);
            //Forming Object
            var Obj = new MaxCodeResult();
            //Check On It
            if (LastTask is null)
            {
                Obj.MaxCode = "TSK001";
                return Obj;
            }
            //Extract Code
            var Code = LastTask.Code;
            //Extract Numeric Part
            int.TryParse(Code.Split("K")[1], out var numericPart);
            //Increment Number
            numericPart = numericPart + 1;
            //Compine Code
            var NewCode = $"TSK{numericPart.ToString().PadLeft(3, '0')}";
            Obj.MaxCode = NewCode;
            //Return Code
            return Obj;
        }
        public async Task<DataWithPagination<ICollection<TaskToReturnDto>>> GetAllTasks(TaskParameters parameters)
        {
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Forming Spec
            var Spec = new TasksListSpecifications(parameters);
            //Get All Data
            var TasksList = await Repo.GetAllAsync(Spec);
            //Mapping Data
            var mappedData = _mapper.Map<ICollection<TaskToReturnDto>>(TasksList);
            //get Total Records
            var TotalRecords = await Repo.GetDataCountAsync(Spec);
            //Forming Result
            var Result = new DataWithPagination<ICollection<TaskToReturnDto>>(parameters.PageNumber, parameters.PageNumber + 1, parameters.PageSize, TotalRecords, mappedData);
            return Result;
        }

        public async Task<TaskDetailsToReturnDto> GetTaskById(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Create Spec
            var Spec = new TaskByIdSpecificatiion(Id);
            //Get Task
            var Task = await Repo.GetByIdAsync(Spec);
            //Check On Task
            if (Task is null) throw new NotFoundException("Task Not Exist!");
            //Mapping Data
            var mappedData = _mapper.Map<TaskDetailsToReturnDto>(Task);
            return mappedData;
        }

        public async Task<ActionStatusDto> PullingTask(TaskToPullDto? data)
        {
            //Check On data
            if (data is null) throw new BadRequestException("Invalid Data");
            //Check On Specific Data
            _ = data switch
            {
                { TaskId: null or ""} => throw new BadRequestException("Task Id Invalid!"),
                { EmployeeId: null or ""} => throw new BadRequestException("Employee Id Invalid!"),
                { TodaysWorkedHours: null or <= 0} => throw new BadRequestException("Invalid Worked Hours!"),
                _ => data
            };
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Forming Spec
            var Spec = new TaskByIdSpecificatiion(data.TaskId);
            //get Task
            var existingTask = await Repo.GetByIdAsync(Spec);
            //Chcek On Task
            if (existingTask is null) throw new NotFoundException("Task Not Exist!");
            //Check If The Assigned Employee is The Same 
            if (existingTask.EmployeeId != data.EmployeeId) throw new ForbiddenException("This Task Is Assigned On Different Employee!");
            //Check If Today is After End Date
            if (new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > existingTask.EndDate) throw new BadRequestException("Can't Pull Task Because Todays Date Exceded Task EndDate!");
            //Check if pulled Hours Number > Task Hrs
            if (existingTask.TaskHours < data.TodaysWorkedHours || existingTask.RemainingHours < data.TodaysWorkedHours) throw new ConflictException("Number of Hours You Have Entered Exceeded Task Hour Budget!");
            //Check If LastPull Exist
            if(existingTask.LastPull is null || existingTask.LastPull != DateTime.Now)
            {
                //Make Log For Last Pull
                existingTask.LastPull = DateTime.Now;
                //Log Worked Hours
                existingTask.WorkedHours += data.TodaysWorkedHours.Value;
                //Log Last Used Hrs
                existingTask.LastUsedHours = data.TodaysWorkedHours.Value;
                //Deduct The Hours From Remaining
                existingTask.RemainingHours = existingTask.RemainingHours - data.TodaysWorkedHours.Value;
            }
            else if(existingTask.LastPull is not null && existingTask.LastPull == DateTime.Now)
            {
                //Log Last Pull
                existingTask.LastPull = DateTime.Now;
                //Add The Worked Hrs To Remaining
                existingTask.RemainingHours += existingTask.LastUsedHours!.Value;
                //Deduct Last Used Hours From Worked Hours
                existingTask.WorkedHours -= existingTask.LastUsedHours.Value;
                //Reset Last Used Hrs
                existingTask.LastUsedHours = data.TodaysWorkedHours.Value;
                //Log New Worked Hours
                existingTask.WorkedHours += data.TodaysWorkedHours.Value;
                //Deduct The Hours From Remaining
                existingTask.RemainingHours = existingTask.RemainingHours - data.TodaysWorkedHours.Value;
            }
            //Update
            Repo.Update(existingTask);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task Pulled Successfully!"
            };
            return Obj;
        }

        public async Task<ActionStatusDto> ReOpenTask(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Create Spec
            var Spec = new TaskByIdSpecificatiion(Id);
            //Get Task
            var Task = await Repo.GetByIdAsync(Spec);
            //Check On Task
            if (Task is null) throw new NotFoundException("Task Not Exist!");
            //Check If Already Closed
            if (Task.Status != Domain.Entities.Tasks.TaskStatus.Closed) throw new ConflictException("Task Must Be Cloased To Open It!");
            //Close Task
            Task.Status = Domain.Entities.Tasks.TaskStatus.ReOpened;
            //Update
            Repo.Update(Task);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Form Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task ReOpened Successfully!"
            };
            return Obj;
        }

        public async Task<ActionStatusDto> UnArchiveTask(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Create Spec
            var Spec = new TaskByIdSpecificatiion(Id);
            //Get Task
            var Task = await Repo.GetByIdAsync(Spec);
            //Check On Task
            if (Task is null) throw new NotFoundException("Task Not Exist!");
            //Check If Already Archived
            if (!Task.isArchived) throw new ConflictException("Task Already UnArchived");
            //Update isArchived
            Task.isArchived = false;
            //Update
            Repo.Update(Task);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Form Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task UnArchived Successfully!"
            };
            return Obj;
        }

        public async Task<ActionStatusDto> UpdateTask(TaskToUpdateDto? task)
        {
            //Check On Data
            if (task is null) throw new BadRequestException("Invalid Data!");
            //Check On Specific Data
            _ = task switch
            {
                { Id: null or "" } => throw new BadRequestException("Invalid Id"),
                { Code: var code } when !Regex.IsMatch(code, codePattern) => throw new BadRequestException("Ivalid Code"),
                { Name: null or "" } => throw new BadRequestException("Must Provide Name"),
                { EmployeeId: null or "" } => throw new BadRequestException("Must Provide Employee"),
                { AssignedHours: <= 0 } => throw new BadRequestException("Invalid Task Hours"),
                _ => task
            };
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Tasks, string>();
            //Forming Spec
            var Spec = new TaskByIdSpecificatiion(task.Id);
            //Get The Task
            var existingTask = await Repo.GetByIdAsync(Spec);
            //Check On existing Task
            if (existingTask is null) throw new NotFoundException("Task Not Exist!");
            //Get Hours Differenc
            var HoursDifference = existingTask.TaskHours > task.AssignedHours ? existingTask.TaskHours - task.AssignedHours : task.AssignedHours - existingTask.TaskHours;
            //Check On The Time Amount
            //Project
            if (existingTask.Type == TaskType.Project)
            {
                if (string.IsNullOrEmpty(existingTask.ProjectId)) throw new BadRequestException("Must Provid Project!");
                //Create Repo
                var ProjRepo = _unitOfWork.GenerateRepository<Project, string>();
                //Create Spec
                var ProjSpec = new ProjectByIdSpecification(existingTask.ProjectId);
                //Get Proje t
                var project = await ProjRepo.GetByIdAsync(ProjSpec);
                //Check On It
                if (project is null) throw new NotFoundException("Project Not Exist!");
                //Check If The Project Hrs < Task Hrs
                if (task.AssignedHours > existingTask.TaskHours && HoursDifference > project.HoursAmount) throw new ConflictException("Task Hours Exceeded Project Hours, Must Be Equal Or Less Than Project Hours!");
                //Update Project Hours
                project.HoursAmount = task.AssignedHours > existingTask.TaskHours ? project.HoursAmount - HoursDifference : project.HoursAmount + HoursDifference;
                //Update
                ProjRepo.Update(project);
            }
            //Ticket
            if (existingTask.Type == TaskType.Ticket)
            {
                //Check If There Is Project | Ticket Exist
                if (string.IsNullOrEmpty(existingTask.TicketId)) throw new BadRequestException("Must Provid Ticket!");
                //Create Repo
                var TickRepo = _unitOfWork.GenerateRepository<ticket, string>();
                //Create Spec
                var TickSpec = new TicketByIdSpecification(existingTask.TicketId);
                //Get Proje t
                var ticket = await TickRepo.GetByIdAsync(TickSpec);
                //Check On It
                if (ticket is null) throw new NotFoundException("Ticket Not Exist!");
                //Check If The Project Hrs < Task Hrs
                if (task.AssignedHours > existingTask.TaskHours && HoursDifference > ticket.HoursNumber) throw new ConflictException("Task Hours Exceeded Ticket Hours, Must Be Equal Or Less Than Ticket Hours!");
                //Update Ticket Hours
                ticket.HoursNumber = task.AssignedHours > existingTask.TaskHours ? ticket.HoursNumber - HoursDifference : ticket.HoursNumber + HoursDifference;
                //Update
                TickRepo.Update(ticket);
            }
            
            //Creating MappedData
            var mappedData = _mapper.Map(task, existingTask);
            //Add Task
            Repo.Update(mappedData);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Create Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Task Updated Successfully!"
            };
            return Obj;
        }
    }
}
