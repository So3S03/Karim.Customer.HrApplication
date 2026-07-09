using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Tickets;
using ticket = Karim.Customer.HrApplication.Domain.Entities.Tickets.Ticket;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Tickets;
using MapsterMapper;
using Karim.Customer.HrApplication.Application.Specifications.Tickets;
using Karim.Customer.HrApplication.Shared.Exceptions;
using System.Text.RegularExpressions;
using Karim.Customer.HrApplication.Domain.Entities.Tickets;
using Karim.Customer.HrApplication.Application.Specifications.Projects;
using Karim.Customer.HrApplication.Domain.Entities.Projects;

namespace Karim.Customer.HrApplication.Application.Services.Ticket
{
    internal class TicketService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITicketServices
    {
        private const string codePattern = @"^TIK\d{3,}$";

        public async Task<MaxCodeResult> GenerateTicketCode()
        {
            //Craete Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Create Spec
            var Spec = new MaxCodeTicketSpecification();
            //get Ticket
            var LastAddedTicket = await Repo.GetByIdAsync(Spec);
            //Create Obj
            var Obj = new MaxCodeResult();
            //Check On It 
            if(LastAddedTicket == null)
            {
                Obj.MaxCode = "TIK001";
                return Obj;
            }
            //Extract Code
            var lastCode = LastAddedTicket.TicketCode;
            //Extract Numeric Part
            int.TryParse(lastCode.Split("K")[1], out int numericPart);
            //Increment NumericPart
            numericPart = numericPart + 1;
            //Set New Code
            Obj.MaxCode = $"TIK{numericPart.ToString().PadLeft(3, '0')}";
            return Obj;
        }
        public async Task<ActionStatusDto> AddNewTicket(TicketToAddDto? data)
        {
            //Check On data
            if (data is null) throw new BadRequestException("Invalid Data!");
            //Check On Specific Data
            _ = data switch
            {
                { TicketCode: null or "" } => throw new BadRequestException("Invalid Code"),
                { TicketCode: var code } when !Regex.IsMatch(code, codePattern) => throw new BadRequestException("Invalid Code"),
                { Name: null or "" } => throw new BadRequestException("Invalid Name"),
                { HoursNumber: <= 0 } => throw new BadRequestException("Tickets Hours Value Must Be Greater Than 0"),
                { ProjectId: null or "" } => throw new BadRequestException("Must Select Project That Has The Issue"),
                _ => data
            };
            //Create Project Repo
            var ProjectRepo = _unitOfWork.GenerateRepository<Domain.Entities.Projects.Project, string>();
            //Create Project Spec
            var ProjSpec = new ProjectByIdSpecification(data.ProjectId);
            //Get Project
            var Project = await ProjectRepo.GetByIdAsync(ProjSpec);
            //Check On Project
            if(Project is null) throw new BadRequestException("Can't Create Ticket For Non-Existent Project!");
            //Check On Project STATUS
            switch(Project.ProjectStatus)
            {
                case Domain.Entities.Projects.ProjectStatus.Draft:
                    throw new ConflictException("Can't Create Ticket On Draft Project, Activate Project First!");
                case Domain.Entities.Projects.ProjectStatus.Cancelled:
                    throw new ConflictException("Can't Create Ticket On Cancelled Project!");
                case Domain.Entities.Projects.ProjectStatus.OnHold:
                    throw new ConflictException("Can't Create Ticket On OnHold Project!");
            }
            //Create Ticket Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Create Code Spec
            var CodeSpec = new TicketByCodeSpecification(data.TicketCode);
            //Get Ticket
            var Tiket = await Repo.GetByIdAsync(CodeSpec);
            //Check If Code Already Exist
            if (Tiket is not null) throw new ConflictException("Ticket Code Already Exist!");
            //Mapping Data
            var mappedData = _mapper.Map<ticket>(data);
            //Add Data
            await Repo.AddAsync(mappedData);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Ticket Created Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> ArchiveTicket(string? ticketId)
        {
            //Check On data
            if (string.IsNullOrEmpty(ticketId)) throw new BadRequestException("Invalid Id");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Forming Specification
            var Spec = new TicketByIdSpecification(ticketId);
            //Get Ticket
            var Ticket = await Repo.GetByIdAsync(Spec);
            //Check On Ticket
            if (Ticket is null) throw new NotFoundException("Ticket Not Exist!");
            //Check If Ticket Already Archived
            if (Ticket.IsArchive) throw new ConflictException("This Ticket Already Archived!");
            //Check If Has Tasks On Going && Status == InProgress
            if (Ticket.Status == TicketStatus.InProgres) throw new BadRequestException("Can't Archive Ticket With Ongoing Tasks!");
            //Archiving Ticket
            Ticket.IsArchive = true;
            //Update Ticket
            Repo.Update(Ticket);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Archived Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> DeleteTicket(string? ticketId)
        {
            //Check On data
            if (string.IsNullOrEmpty(ticketId)) throw new BadRequestException("Invalid Id");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Forming Specification
            var Spec = new TicketByIdSpecification(ticketId);
            //Get Ticket
            var Ticket = await Repo.GetByIdAsync(Spec);
            //Check On Ticket
            if (Ticket is null) throw new NotFoundException("Ticket Not Exist!");
            //Check If Has Tasks On Going && Status == InProgress
            if (Ticket.Status == TicketStatus.InProgres) throw new BadRequestException("Can't Archive Ticket With Ongoing Tasks!");
            //Delete
            Repo.Delete(Ticket);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Deleted Successfully!"
            };
            return Obj;

        }
        public async Task<DataWithPagination<ICollection<TicketToReturnDto>>> GetAllTickets(TicketsParameter parameters)
        {
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Forming Spec
            var Spec = new TicketListSpecification(parameters);
            //Get All Tickets
            var TicketsList = await Repo.GetAllAsync(Spec);
            //Get Count
            var Count = await Repo.GetDataCountAsync(Spec);
            //Mapping Data
            var MappedList = _mapper.Map<ICollection<TicketToReturnDto>>(TicketsList);
            //Forming Obj
            var Obj = new DataWithPagination<ICollection<TicketToReturnDto>>(parameters.PageNum, parameters.PageNum + 1, parameters.PageSize, Count, MappedList); //todo: need Modification
            return Obj;
        }
        public async Task<TicketDetailsToReturnDto> GetSpecificTicketDetails(string? ticketId)
        {
            //Check On data
            if (string.IsNullOrEmpty(ticketId)) throw new BadRequestException("Invalid Id");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Forming Specification
            var Spec = new TicketByIdSpecification(ticketId);
            //Get Ticket
            var Ticket = await Repo.GetByIdAsync(Spec);
            //Check On Ticket
            if (Ticket is null) throw new NotFoundException("Ticket Not Exist!");
            //Mapping Data
            var mappedData = _mapper.Map<TicketDetailsToReturnDto>(Ticket);
            //return data
            return mappedData;
        }
        public async Task<ActionStatusDto> UndoArchiveTicket(string? ticketId)
        {
            //Check On data
            if (string.IsNullOrEmpty(ticketId)) throw new BadRequestException("Invalid Id");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Forming Specification
            var Spec = new TicketByIdSpecification(ticketId);
            //Get Ticket
            var Ticket = await Repo.GetByIdAsync(Spec);
            //Check On Ticket
            if (Ticket is null) throw new NotFoundException("Ticket Not Exist!");
            //Check If Ticket Already Archived
            if (Ticket.IsArchive == false) throw new ConflictException("This Ticket Already UnArchived!");
            //Archiving Ticket
            Ticket.IsArchive = false;
            //Update Ticket
            Repo.Update(Ticket);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "UnArchived Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> UpdateTicket(TicketToUpdateDto? data)
        {
            //Check On data
            if (data is null) throw new BadRequestException("Invalid Data!");
            //Check On Specific Data
            _ = data switch
            {
                { Id: null or ""} => throw new BadRequestException("Invalid Id!"),
                { TicketCode: var code } when !Regex.IsMatch(code, codePattern) => throw new BadRequestException("Invalid Code"),
                { Name: null or "" } => throw new BadRequestException("Invalid Name"),
                { HoursNumber: <= 0 } => throw new BadRequestException("Tickets Hours Value Must Be Greater Than 0"),
                _ => data
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Create Code Spec
            var CodeSpec = new TicketByIdSpecification(data.Id);
            //Get Ticket
            var Tiket = await Repo.GetByIdAsync(CodeSpec);
            //Check If Code Already Exist
            if (Tiket is null) throw new NotFoundException("Ticket Not Exist!");
            //Check On Code
            if (Tiket.TicketCode != data.TicketCode) throw new BadRequestException("Registered Code Not Match The Provided Code!");
            //Get Project
            var Project = Tiket.Project;
            //Check On Project
            if (Project is null) throw new ConflictException("Can't Adjust Ticket Have No Project!");
            //Check On Project Status
            switch (Project.ProjectStatus)
            {
                case Domain.Entities.Projects.ProjectStatus.Draft:
                    throw new ConflictException("Can't Update Ticket That Have Draft Project, Activate Project First!");
                case Domain.Entities.Projects.ProjectStatus.Cancelled:
                    throw new ConflictException("Can't Update Ticket That Have Cancelled Project!");
                case Domain.Entities.Projects.ProjectStatus.OnHold:
                    throw new ConflictException("Can't Update Ticket That Have OnHold Project!");
            }
            //Mapping Data
            var mappedData = _mapper.Map(data, Tiket);
            //Add Data
            Repo.Update(mappedData);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Ticket Updated Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> CloseTicket(string? ticketId)
        {
            //Check On data
            if (string.IsNullOrEmpty(ticketId)) throw new BadRequestException("Invalid Id");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Forming Specification
            var Spec = new TicketByIdSpecification(ticketId);
            //Get Ticket
            var Ticket = await Repo.GetByIdAsync(Spec);
            //Check On Ticket
            if (Ticket is null) throw new NotFoundException("Ticket Not Exist!");
            //Check If Ticket Already Closed
            if (Ticket.Status == TicketStatus.Closed) throw new ConflictException("Ticket Already Closed");
            //Change Status
            Ticket.Status = TicketStatus.Closed;
            //Update
            Repo.Update(Ticket);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Ticket Closed Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> ReOpenTicket(string? ticketId)
        {
            //Check On data
            if (string.IsNullOrEmpty(ticketId)) throw new BadRequestException("Invalid Id");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<ticket, string>();
            //Forming Specification
            var Spec = new TicketByIdSpecification(ticketId);
            //Get Ticket
            var Ticket = await Repo.GetByIdAsync(Spec);
            //Check On Ticket
            if (Ticket is null) throw new NotFoundException("Ticket Not Exist!");
            //Check If Ticket Already Closed
            if (Ticket.Status != TicketStatus.Closed) throw new ConflictException("Can't Open Non Closed Ticket");
            //Change Status
            Ticket.Status = TicketStatus.Opened;
            //Update
            Repo.Update(Ticket);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Ticket Opened Successfully!"
            };
            return Obj;
        }
    }
}
