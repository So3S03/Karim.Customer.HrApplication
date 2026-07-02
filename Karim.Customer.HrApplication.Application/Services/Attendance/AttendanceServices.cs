using Karim.Customer.HrApplication.Application._Common.DateConverter;
using Karim.Customer.HrApplication.Application._Common.EnumConverter;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance;
using Karim.Customer.HrApplication.Application.Specifications.Attendance;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using Karim.Customer.HrApplication.Domain.Conttracts;
using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance.BulkDtos;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Services.Attendance
{
    internal class AttendanceServices(IUnitOfWork _unitOfWork, IMapper mapper, IExcelServices excelServices) : IAttendanceServices
    {
        public async Task<SpecificFingerprintToReturnDto> GetFingerprintPerEmployeeForCurrentDay(string? EmpId)
        {
            //Check On Data
            if (EmpId == null) throw new BadRequestException("Employee Id Must Be Provided");
            //Forming Date
            var Today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            //Get Fingerprint
            var fingerprint = await getFingerPrint(EmpId);
            //Check On Fingerprint
            if (fingerprint is null)
            {
                var emptyObject = new SpecificFingerprintToReturnDto()
                {
                    Id = null,
                    EmpId = EmpId,
                    EmployeeName = fingerprint?.Employee.FullName ?? "",
                    CheckIn = null,
                    CheckOut = null,
                    Date = Today,
                    CheckInLat = null,
                    CheckInLong = null,
                    CheckOutLat = null,
                    CheckOutLong = null,
                    Status = FingerprintSatusLockup.Absense.ToString()
                };
                return emptyObject;
            }
            //Foming Mapped Fingerprint
            var mappedFinger = mapper.Map<SpecificFingerprintToReturnDto>(fingerprint);
            return mappedFinger;
        }
        public async Task<ActionStatusDto> InsertFingerprint(FingerprintToBeInsertDto? fingerprint)
        {
            //Check On Data
            if (fingerprint == null) throw new BadRequestException("Invalid Data, Please Try Again Later");
            //Check On EmpId
            if (string.IsNullOrEmpty(fingerprint.EmpId)) throw new BadRequestException("Employee Id Must Be Provided!");
            //Get Today Date
            var Today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            //Get Fingerprint Details
            var existingFingerprint = await getFingerPrint(fingerprint.EmpId);
            //Forming Repos
            var Repo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Forming Emp Repo
            var empRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Forming Specification
            var Spec = new EmployeeByIdSepecification(fingerprint.EmpId);
            //Get Employee To Update His Status
            var Employee = existingFingerprint is null ?  await empRepo.GetByIdAsyncWithNoTracking(Spec) : null;
            //Check On Employee 
            if (Employee is null && existingFingerprint is null) throw new NotFoundException("Employee Not Exist");
            //Forming Object
            var mappedFingerprint = mapper.Map<Fingerprint>(fingerprint);
            //Check On Fingerprint
            if(existingFingerprint is null) //Mean That there is no fingerprint for today
            {
                if (Employee.EmployeeStatus == EmployeeStatus.Terminated) throw new BadRequestException("Can't Insertfingerprint This Employee is Terminated!");
                mappedFingerprint.CheckIn = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                mappedFingerprint.CheckOut = null;
                mappedFingerprint.Date = Today;
                mappedFingerprint.DurationInHours = 0;
                mappedFingerprint.Status = DateTime.Now.Hour > 9 ? FingerprintStatus.Late : FingerprintStatus.Active;
                mappedFingerprint.CheckInLong = fingerprint.Long;
                mappedFingerprint.CheckInLat = fingerprint.Lat;
                mappedFingerprint.CheckOutLat = null;
                mappedFingerprint.CheckOutLong = null;
                Employee.EmployeeStatus = DateTime.Now.Hour > 9 ? EmployeeStatus.Late : EmployeeStatus.Active;
                await Repo.AddAsync(mappedFingerprint);
                empRepo.Update(Employee);
                
            }
            else
            {
                if (existingFingerprint.Employee.EmployeeStatus == EmployeeStatus.Terminated) throw new BadRequestException("Can't Insertfingerprint This Employee is Terminated!");
                var Duration = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute) - existingFingerprint.CheckIn;
                existingFingerprint.CheckOut = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                existingFingerprint.DurationInHours = (decimal)Duration.TotalHours;
                existingFingerprint.CheckOutLat = fingerprint.Lat;
                existingFingerprint.CheckOutLong = fingerprint.Long;
                existingFingerprint.Status = existingFingerprint.CheckIn.Hour > 9 ? FingerprintStatus.Late : (DateTime.Now.Hour - existingFingerprint.CheckIn.Hour < 8 ? FingerprintStatus.Delay : FingerprintStatus.InActive);
                existingFingerprint.Employee.EmployeeStatus = existingFingerprint.CheckIn.Hour > 9 ? EmployeeStatus.Late : (DateTime.Now.Hour - existingFingerprint.CheckIn.Hour < 8 ? EmployeeStatus.Delay : EmployeeStatus.InActive);
                Repo.Update(existingFingerprint);
                empRepo.Update(existingFingerprint.Employee);
            }
            //Complete
            bool result = await _unitOfWork.CompleteAsync() > 0;
            //Check If Saved
            if (!result) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = existingFingerprint is null ? "Checked in Sucessfully" : "Checked Out Successfully"
            };
            //return
            return Obj;
        }
        public ICollection<EnumDto> GetFingerPrintStatusLockup()
        {
            //Make List
            var list = EnumsConvertion.CreateEnumLists<FingerprintSatusLockup>();
            //return them
            return list;
        }
        public async Task<FingerprintDetailsToReturnDto> GetFingerprintById(string? Id)
        {
            //Check On Id
            if (string.IsNullOrEmpty(Id)) throw new BadRequestException("Provided Id is Invalid");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Forming Specification
            var Spec = new FingerprintByIdSpecification(Id);
            //Get Fingerprint
            var Fingerprint = await Repo.GetByIdAsync(Spec);
            //Check On It
            if (Fingerprint == null) throw new NotFoundException("Fingerprint Not Exist");
            //Mapping It
            var MappedFB = mapper.Map<FingerprintDetailsToReturnDto>(Fingerprint);
            //return it
            return MappedFB;
        }
        public async Task<DataWithPagination<ICollection<FingerprintToReturnDto>>> GetAllFingerprintLogs(FingerprintParameters? fingerprintParameters)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Create Specification
            var Specification = new FinerprintListSpecifications(fingerprintParameters);
            //Create Specification For Count
            var countSpec = new FingerprintCountSpecification(fingerprintParameters);
            //Get List
            var AttendanceList = await Repo.GetAllAsync(Specification);
            //Get Count
            var Count = await Repo.GetDataCountAsync(countSpec);
            //Get Pages Count
            var pagesCount = Math.Ceiling((decimal)Count / fingerprintParameters.PageSize);
            //Mapping List
            var mappeedList = mapper.Map<ICollection<FingerprintToReturnDto>>(AttendanceList);
            //Forming Pagination Object
            var PaginatedData = new DataWithPagination<ICollection<FingerprintToReturnDto>>(
                pageNum: fingerprintParameters.PageNum,
                nextPage: pagesCount < (fingerprintParameters.PageNum + 1) ? pagesCount : (fingerprintParameters.PageNum + 1),
                pageSize: fingerprintParameters.PageSize,
                totalRecords: Count,
                data: mappeedList);
            return PaginatedData;
        }
        public async Task<ActionStatusDto> InsertFingerprintManualyForEmployee(FingerprintToAddDto? fingerprint)
        {
            //Check On Data
            if (fingerprint is null) throw new BadRequestException("Provided Data is Invalid!");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Check If Employee Has Fingerprint On The Selected Date
            //Foming Spec
            var Spec = new TodaysFingerprintByEmpIdSpecification(fingerprint.EmpId, fingerprint.Date);
            //Get Fingerprint
            var existedFingerprint = await Repo.GetByIdAsync(Spec);
            //Check On It
            if (existedFingerprint is not null) throw new ConflictException("Can't Add Multible Fingerprints For Same Employee In The Same Day");   
            //Mapping Data
            var MappedData = mapper.Map<Fingerprint>(fingerprint);
            //Calc Duration
            MappedData.DurationInHours = fingerprint.CheckOut.HasValue ? (decimal)(fingerprint.CheckOut!.Value - fingerprint.CheckIn).TotalHours : 0;
            //Add Data
            await Repo.AddAsync(MappedData);
            //Complete
            bool complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!complete) throw new Exception("Something Went Wrong!");
            //Formming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Fingerprint Added Successully"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> EditEmployeeFingerprint(FingerprintToUpdateDto? fingerprint)
        {
            //Check On Data
            if (fingerprint is null) throw new BadRequestException("Provided Data is Invalid!");
            //Check On Properties
            _ = fingerprint switch
            {
                { EmpId : "" or null} => throw new BadRequestException("Emp Id Must Be Provided!"),
                { Id: "" or null} => throw new BadRequestException("Fingerprint Id Must Be Provided!"),
                _ => fingerprint
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Create Specification
            var Spec = new FingerprintById(fingerprint.Id);
            //Get Fingerprint
            var Fingerprint = await Repo.GetByIdAsyncWithNoTracking(Spec);
            //Check On Fingerprint
            if (Fingerprint is null) throw new NotFoundException("No Fingerprint Exist With This Id!");
            //Check On Employee
            if (Fingerprint.Employee.EmployeeStatus.Value == EmployeeStatus.Terminated) throw new ConflictException("Can't Modify Terminated Employees Fingerprints");
            //Check If Comming EmpId == Existing EmpId
            if (Fingerprint.EmpId != fingerprint.EmpId) throw new ConflictException("Provided Employee Not Match The Fingerprint Employee");
            //Mapping Data
            var mappedData = mapper.Map(fingerprint, Fingerprint);
            mappedData.DurationInHours = fingerprint.CheckOut.HasValue ? (decimal)(fingerprint.CheckOut.Value - fingerprint.CheckIn).TotalHours : Fingerprint.DurationInHours;
            mappedData.Date = Fingerprint.Date;
            mappedData.Status = fingerprint.CheckIn > new TimeOnly(9, 0) ?
                FingerprintStatus.Late :
                (fingerprint.CheckOut.HasValue == false ?
                FingerprintStatus.Active :
                (decimal)(fingerprint.CheckOut.Value - fingerprint.CheckIn).TotalHours < 8 ?
                FingerprintStatus.Delay : FingerprintStatus.InActive);
            //Update
            Repo.Update(mappedData);
            //Change Employee Status If Fingerprint is For Today
            if (Fingerprint.Date == new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                //Update Employee
                Fingerprint.Employee.EmployeeStatus = fingerprint.CheckIn > new TimeOnly(9, 0) ?
                    EmployeeStatus.Late :
                    (fingerprint.CheckOut.HasValue == false ?
                    EmployeeStatus.Active :
                    (decimal)(fingerprint.CheckOut.Value - fingerprint.CheckIn).TotalHours < 8 ?
                    EmployeeStatus.Delay : EmployeeStatus.InActive);
                var empRepo = _unitOfWork.GenerateRepository<employee, string>();
                empRepo.Update(Fingerprint.Employee);
            };
            //Complete
            var result = await _unitOfWork.CompleteAsync() > 0;
            //Check On result
            if (!result) throw new Exception("Something Went Wrong");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Fingerprint Updated Successfully"
            };
            return Obj;
        }
        public byte[] GetUploadFingerprintBulk()
        {
            //Forming Example
            var example = new AddCheckInBulkDto()
            {
                CheckIn = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                CheckOut = new TimeOnly(DateTime.Now.Hour + 1, DateTime.Now.Minute, DateTime.Now.Second),
                EmpCode = "EMP001"
            };
            //Forming File
            var file = excelServices.GenerateExcelSheetTemplate(example, "UploadBulkFingerprint");
            return file;
        }
        public async Task<ActionStatusDto> UploadBulkFingerprintDto(IFormFile? file)
        {
            //Check File
            if (file is null) throw new BadRequestException("Must Provid File!");
            //Read File
            var list = excelServices.ReadExcelSheetForCollections<AddCheckInBulkDto>(file);
            //Check On List
            if (list.Any(e => e.CheckIn is null || e.CheckOut is null || e.EmpCode is null)) throw new BadRequestException("One Or More Field is Empty!");
            //Check For Any Dublication
            if (list.Where(e => e is not null).GroupBy(e => e.EmpCode).Any(e => e.Count() > 1)) throw new ConflictException("There Are Dupliacted Employee Code");
            //Put Codes On List
            var CodeList = list.Where(e => e is not null).Select(e => e.EmpCode).ToList();
            //Create EmpRepo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Forming Spec
            var EmpSpec = new AllEmployeesByCodesSpec(CodeList!);
            //Get All Employees
            var EmployeesList = await EmpRepo.GetAllAsync(EmpSpec);
            //Check If Emp List is null
            if (!EmployeesList.Any()) throw new NotFoundException("Some of the selected employees not exist!");
            //Get All Employees Id Where Emp Code Exist On It
            var EmpsIdWithCode = EmployeesList.Where(E => CodeList.Contains(E.EmployeeCode)).Select(E => new
            {
                Id = E.Id,
                EmpCode = E.EmployeeCode
            }).ToList();
            //Generate New List 
            var newList = new HashSet<AddCheckInBulkDto>();
            foreach (var e in list)
            {
                e.EmpCode = EmpsIdWithCode.First(s => s.EmpCode == e.EmpCode)!.Id;
                newList.Add(e);
            }
            //Mapping Data
            var mappedFingeerprints = mapper.Map<ICollection<Fingerprint>>(newList); 
            //Create Fingerprint Repo
            var FPRepo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //AddRange Fingerprints
            await FPRepo.AddRangeAsync(mappedFingeerprints);
            //Compleate
            var complete = await _unitOfWork.CompleteAsync();
            //Check on Complete
            if (complete == 0) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Fingerprints Added Successfully!"
            };
            return Obj;
        }



        //Need Fixing
        public async Task<EmployeeAttendanceStatusDto> GetAttendanceSummaryPerEmployeeForCurrentMonth(string? EmpId)
        {
            //Check On Data
            if (EmpId is null) throw new BadRequestException("Employee Id Must Be Provided");
            //Form Employee Repo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Forming Spec
            var EmpSpec = new EmployeeByIdSepecification(EmpId);
            //Get Employee
            var Employee = await EmpRepo.GetByIdAsync(EmpSpec);
            //Check If Employee Exist
            if (Employee is null) throw new NotFoundException("Employee Not Exist");
            //Forming FingerprintRepo
            var FPRepo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Forming Spec
            var FPSpec = new FingerprintSummarySpecification(EmpId, new DateOnly(DateTime.Today.Year, DateTime.Today.Month, 1), new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Now.Day));
            //Get Status
            var FPSummary = await FPRepo.GetProjectedAsync<FingerprintStatus, FingerprintSummaryDto>(FPSpec);
            //Get Requests Summary
            //...Need To Create Requests First

            //Forming Object
            var Obj = new EmployeeAttendanceStatusDto()
            {
                FingerprintSummary = FPSummary,
                RequestsSummary = new RequestsSummryDto(),
                AbsentCount = 0,
                AttendancePercentage = 0,
                TotalAttendanceDays = 0
            };
            return Obj;
        }

        public async Task<ActionStatusDto> CreateRequest(RequestToAddDto? request)
        {
            //Check On Data
            if(request is null) throw new BadRequestException("Provided Data is Invalid!");
            //Check On Internal Data
            _ = request switch
            {
                { EmpId: "" or null } => throw new BadRequestException("Employee Id Must Be Provided!"),
                { Type: var t } when !Enum.IsDefined(typeof(RequestType), t) => throw new BadRequestException("Request Type Is Invalid!"),
                _ => request
            };
            //Genearete Employee Repo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Generate Spec
            var EmpSpec = new EmployeeByIdSepecification(request.EmpId);
            //Get Employee
            var Employee = await EmpRepo.GetByIdAsync(EmpSpec);
            //Check On Employee If Exist
            if (Employee is null) throw new NotFoundException("Employee You Try To Add Request For is Not Exist");
            //Store FPID
             string? FPID = null;
            //Check On Request Type & Make Start Date = End Date
            if ((RequestType)request.Type != RequestType.Vacation)
            {
                request.EndDate = request.StartDate;
                //Forming Fingerprint Repo
                var FPRepo = _unitOfWork.GenerateRepository<Fingerprint, string>();
                //Forming Spec
                var FPSpec = new TodaysFingerprintByEmpIdSpecification(request.EmpId, request.StartDate);
                //Get Fingerprint
                var FP = await FPRepo.GetByIdAsync(FPSpec);
                //Check If There Fingerprint For The Employee On The Same Date
                if (FP is null) throw new ConflictException("There Is No Fingerprint For The Employee On This Date To Register This Request!");
                //Add Fingerprint Id To Request
                FPID = FP.Id;
            }
            //Check On Overtime
            if((RequestType)request.Type == RequestType.Overtime)
            {
                //Check If The Overtime Hours Added
                if (request.Duration is null) throw new BadRequestException("Must Provide Overtime Hours!");
            }
            //Forming Requests Repo
            var ReqRepo = _unitOfWork.GenerateRepository<Requests, string>();
            //Forming Spec
            var ReqSpec = new RequestByDateAndEmpId(request.EmpId, request.StartDate, request.EndDate);
            //Get Requests
            var ExistingRequests = await ReqRepo.GetAllAsync(ReqSpec);
            //Check If There Any Request Exist On The Same Date
            if (ExistingRequests.Any()) throw new ConflictException("There Is Already A Request On The Same Date");
            //Mapping Data
            var MappedData = mapper.Map<Requests>(request);
            //Add Fingerprint Id To MappedData
            MappedData.FingerprintId = FPID;
            //Add Request
            await ReqRepo.AddAsync(MappedData);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if(!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = $"{MappedData.Type.ToString()} Added Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> EditRequest(RequestToEditDto? request)
        {
            //Check On Data
            if(request is null) throw new BadRequestException("Provided Data is Invalid!");
            //Check On Internal Data
            _ = request switch
            {
                { Id: "" or null } => throw new BadRequestException("Request Id Must Be Provided!"),
                { Type: var t } when !Enum.IsDefined(typeof(RequestType), t) => throw new BadRequestException("Request Type Is Invalid!"),
                _ => request
            };
            //Forming Repo
            var ReqRepo = _unitOfWork.GenerateRepository<Requests, string>();
            //Forming Spec
            var ReqByIdSpec = new EmployeeRequestById(request.Id);
            //Get Request
            var ExistingRequest = await ReqRepo.GetByIdAsyncWithNoTracking(ReqByIdSpec);
            //Check On It
            if(ExistingRequest is null) throw new NotFoundException("Request You Seek To Edit is Not Exist!");
            //Check If The Created Employee != Income Employee
            if(ExistingRequest.EmpId != request.EmpId) throw new NotFoundException("You Can't Edit Someone Else's Request!");
            //Check If Types Are The Same
            if(ExistingRequest.Type != (RequestType)request.Type) throw new ConflictException("Provided Request Type is Wrong");
            //Check If It's Approved Or Rejected
            if (ExistingRequest.ApprovedById is not null) throw new BadRequestException("Can't Modify An Accepted Request");
            if (ExistingRequest.RejectedById is not null) throw new BadRequestException("Can't Modify An Rjected Request");
            //Check If Selected Dates Aren't The Same
            if(request.StartDate != ExistingRequest.StartDate || request.EndDate != ExistingRequest.EndDate)
            {
                //Forming Spec To Get Employee Requests On The Selected Date
                var ReqByDateSpec = new RequestByDateAndEmpId(request.EmpId, request.StartDate, request.EndDate);
                //Get Requests
                var isThereAnyRequests = await ReqRepo.GetAllAsync(ReqByDateSpec);
                //Check If The Selected Date Has Any Other Requests
                if (isThereAnyRequests.Any()) throw new ConflictException("There Is Already A Request On The Same Date");
            }
            //Mapping Data
            var MappedData = mapper.Map(request, ExistingRequest);
            //Update Request
            ReqRepo.Update(MappedData);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if(!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = $"{MappedData.Type.ToString()} Updated Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> DeleteRequest(string? ReqId)
        {
            //Check On Id
            if(string.IsNullOrEmpty(ReqId)) throw new BadRequestException("Request Id Must Be Provided!");
            //Forming Repo
            var ReqRepo = _unitOfWork.GenerateRepository<Requests, string>();
            //Forming Spec
            var RequestSpec = new EmployeeRequestById(ReqId);
            //Get Request
            var ExistingRequest = await ReqRepo.GetByIdAsyncWithNoTracking(RequestSpec);
            //Check On It
            if(ExistingRequest is null) throw new NotFoundException("Request You Seek To Delete is Not Exist!");
            //Delete Request
            ReqRepo.Delete(ExistingRequest);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if(!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = $"{ExistingRequest.Type.ToString()} Deleted Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> ApproveRejectRequest(string? ReqId, bool? isApproved, string? LoginEmpId)
        {
            //Check On Data
            if(string.IsNullOrEmpty(ReqId) || isApproved is null || LoginEmpId is null) throw new BadRequestException("Provided Data is Invalid!");
            //Forming Repo
            var ReqRepo = _unitOfWork.GenerateRepository<Requests, string>();
            //Forming Spec
            var RequestSpec = new EmployeeRequestById(ReqId);
            //Get Request
            var ExistingRequest = await ReqRepo.GetByIdAsyncWithNoTracking(RequestSpec);
            //Forming Status
            var status = isApproved.Value ? "Approve" : "Reject";
            //Check On It
            if (ExistingRequest is null) throw new NotFoundException($"Request You Seek to {status} is Not Exist!");
            //Check If Already Approved Or Rejected
            if(ExistingRequest.Status == RequestStatus.Approved && isApproved.Value == true && ExistingRequest.ApprovedById is not null || ExistingRequest.RejectedById is not null) throw new ConflictException("This Request is Already Approved!");
            else if (ExistingRequest.Status == RequestStatus.Rejected && isApproved.Value == false && ExistingRequest.ApprovedById is not null || ExistingRequest.RejectedById is not null) throw new ConflictException("This Request is Already Rejected!");
            //Check If The Approved/Rejected Person Is The Same Man Who Created The Request
            if (LoginEmpId == ExistingRequest.EmpId) throw new ConflictException("You Can't Approve/Reject Your Own Request");
            //Change Status
            ExistingRequest.Status = isApproved.Value ? RequestStatus.Approved : RequestStatus.Rejected;
            //Create Employee Repo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Forming Spec
            var EmpSpec = new EmployeeByIdSepecification(LoginEmpId);
            //Get Employee
            var Employee = await EmpRepo.GetByIdAsyncWithNoTracking(EmpSpec);
            //Check On Employee
            if (Employee is null) throw new NotFoundException("Your Id Doesn't Match Any Employees On The System!");
            //Change Approver Rejecter Id & Approver Name
            if (isApproved.Value)
            {
                ExistingRequest.ApprovedById = LoginEmpId;
                ExistingRequest.ApprovedByName = Employee.FullName;
            }
            else if (!isApproved.Value)
            {
                ExistingRequest.RejectedById = LoginEmpId;
                ExistingRequest.RejectedByName = Employee.FullName;
            }
            //Update Request
            ReqRepo.Update(ExistingRequest);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = $"Request {status}d Successfully!"
            };
            return Obj;
        }
        public async Task<RequestDetailsToReturnDto> GetRequestDetailsById(string? ReqId)
        {
            //Check On Id
            if (string.IsNullOrEmpty(ReqId)) throw new BadRequestException("Request Id Must Be Provided!");
            //Forming Repo
            var ReqRepo = _unitOfWork.GenerateRepository<Requests, string>();
            //Forming Spec
            var RequestSpec = new EmployeeRequestById(ReqId);
            //Get Request
            var ExistingRequest = await ReqRepo.GetByIdAsync(RequestSpec);
            //Check On It
            if (ExistingRequest is null) throw new NotFoundException("Request You Seek To Get Details For is Not Exist!");
            //Mapping Data
            var MappedData = mapper.Map<RequestDetailsToReturnDto>(ExistingRequest);
            //return it
            return MappedData;
        }
        public async Task<DataWithPagination<ICollection<RequestToReturnDto>>> GetAllRequests(RequestsParameters? parameters)
        {
            //Check For Data
            if (parameters is null || parameters.EmpId is null) throw new BadRequestException("You Should Provide Employee Id");
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Requests, string>();
            //Forming Spec
            var Spec = new AllRequestsSpecification(parameters);
            //Get Requests
            var AllRequests = await Repo.GetAllAsync(Spec);
            //Get Count 
            var Count = await Repo.GetDataCountAsync(Spec);
            //Mapping Data
            var mappedData = mapper.Map<ICollection<RequestToReturnDto>>(AllRequests);
            //Form Object
            var Object = new DataWithPagination<ICollection<RequestToReturnDto>>(
                    pageNum: parameters.PageNum,
                    pageSize: (decimal)parameters.PageSize,
                    nextPage: parameters.PageNum > Math.Ceiling((decimal)(Count / parameters.PageSize)) ? parameters.PageNum : (parameters.PageNum + 1 ),
                    totalRecords: Count,
                    data: mappedData
                );
            return Object;
        }

        private async Task<Fingerprint?> getFingerPrint(string? EmpId)
        {
            //Forming Date
            var Today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Forming Specification
            var Spec = new TodaysFingerprintByEmpIdSpecification(EmpId, Today);
            //Get Fingerprint
            var fingerprint = await Repo.GetByIdAsyncWithNoTracking(Spec);
            //return
            return fingerprint;
        }
    }
}
