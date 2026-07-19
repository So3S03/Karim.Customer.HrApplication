using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Payrolls;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using Karim.Customer.HrApplication.Application.Specifications.Payrolls;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Services.Payrolls
{
    internal class PayrollService(IUnitOfWork _unitOfWork, IMapper _mapper) : IPayrollService
    {
        public async Task<DataWithPagination<ICollection<PayslipToReturnDto>>> GetAllEmployeesPayslipsPerMonth(PayrollParameter parameter)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Specification
            var Spec = new PayrollListSpecification(parameter);
            //Get Data
            var Payslips = await Repo.GetAllAsync(Spec);
            //Mapping Data
            var mappedList = _mapper.Map<ICollection<PayslipToReturnDto>>(Payslips);
            //Get Total Records
            var totalRecords = await Repo.GetDataCountAsync(Spec);
            //Create Pagination Data Object
            var Obj = new DataWithPagination<ICollection<PayslipToReturnDto>>(parameter.PageNum, parameter.PageNum + 1, parameter.PageSize, totalRecords, mappedList);
            return Obj;
        }
        public async Task<DataWithPagination<ICollection<PayslipToReturnDto>>> GetEmployeeAllPayslips(EmployeePayslipsParameter parameter)
        {
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Specification
            var Spec = new SpecificEmployeePayslips(parameter);
            //Get Data
            var Payslips = await Repo.GetAllAsync(Spec);
            //Mapping Data
            var mappedList = _mapper.Map<ICollection<PayslipToReturnDto>>(Payslips);
            //Get Total Records
            var totalRecords = await Repo.GetDataCountAsync(Spec);
            //Create Pagination Data Object
            var Obj = new DataWithPagination<ICollection<PayslipToReturnDto>>(parameter.PageNum, parameter.PageNum + 1, parameter.PageSize, totalRecords, mappedList);
            return Obj;
        }
        public async Task<ActionStatusDto> ApproveSalary(string? PayslipId)
        {
            //Check On Id
            if (PayslipId is null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var Spec = new PayslipById(PayslipId);
            //Get Payslip
            var Payslip = await Repo.GetByIdAsync(Spec);
            //Check On Payslip
            if (Payslip == null) throw new NotFoundException("Payslip Doesn't Exist!");
            //Check If Already Approved
            if (Payslip.Status == PayrollStatus.Approved) throw new ConflictException("This Payroll is Already Approved!");
            //Change Status
            Payslip.Status = PayrollStatus.Approved;
            //Update
            Repo.Update(Payslip);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Salary Approved Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> PaySalary(PayrollToPayDto? payrollToPayDto)
        {
            //Check On Data
            if (payrollToPayDto is null) throw new BadRequestException("Invalid Data");
            _ = payrollToPayDto switch
            {
                { PayslipId: null or ""} => throw new BadRequestException("Invalid Id!"),
                { PaymentWay: var way} when !Enum.IsDefined(typeof(PayrollPaymentWay), way) => throw new BadRequestException("Invalid Payment Way!"),
                _ => payrollToPayDto
            };
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var Spec = new PayslipById(payrollToPayDto.PayslipId);
            //Get Payslip
            var Payslip = await Repo.GetByIdAsync(Spec);
            //Check On Payslip
            if (Payslip is null) throw new NotFoundException("Payslip Doesn't Exist!");
            //Check If Already Paid
            if (Payslip.Status == PayrollStatus.Paid) throw new ConflictException("Salary Already Paid!");
            //Check If Salary Approved Or Not
            if (Payslip.Status != PayrollStatus.Approved) throw new BadRequestException("You Must Approve The Salary Before Paying!");
            //Change Status & Payment Way
            Payslip.Status = PayrollStatus.Paid;
            Payslip.PaymentWay = (PayrollPaymentWay)payrollToPayDto.PaymentWay;
            Payslip.PaidAt = DateTime.Now;
            Payslip.PaidNotes = payrollToPayDto.PaidNote;
            //Update
            Repo.Update(Payslip);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Salary Paid Successfully!"
            };
            return Obj;
        }
        public async Task<PayslipDetailsToReturnDto> GetPayslipDetails(string? PayslipId)
        {
            if (PayslipId is null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var Spec = new PayslipById(PayslipId);
            //Get Payslip
            var Payslip = await Repo.GetByIdAsyncWithNoTracking(Spec);
            //Check On It
            if (Payslip is null) throw new NotFoundException("Payslip Not Exist!");
            //Map Data
            var mappedPayslip = _mapper.Map<PayslipDetailsToReturnDto>(Payslip);
            return mappedPayslip;
        }
        public async Task<ActionStatusDto> AddPenalty(PenaltyToAddDto? penaltyToAddDto)
        {
            //Check On Data
            if (penaltyToAddDto is null) throw new BadRequestException("Invalid Data!");
            //Check On Internal Data
            _ = penaltyToAddDto switch
            {
                { PayslipId: null or ""} => throw new BadRequestException("Invalid PayslipId!"),
                { Title: null or ""} => throw new BadRequestException("Must Add Title For The Penalty!"),
                { Value: <= 0} => throw new BadRequestException("Value Must Be Greater Than Zero"),
                _ => penaltyToAddDto
            };
            //Create Payslip Repo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var PayslipSpec = new PayslipById(penaltyToAddDto.PayslipId);
            //Get Payslip
            var Payslip = await PayslipRepo.GetByIdAsync(PayslipSpec);
            //Check On Payslip
            if (Payslip is null) throw new NotFoundException("Payslip Not Exist!");
            //Check If Not Approved Or Paid
            if (Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Operate On Approved Or Paid Salary!");
            //Check If The Net Salary will be zero or less after deduction
            if (Payslip.NetSalary - penaltyToAddDto.Value < 0) throw new ConflictException("Penalty exceeds remaining net salary!");
            //Update The Net Salary
            Payslip.NetSalary = Payslip.NetSalary - penaltyToAddDto.Value;
            //Update Payslip
            PayslipRepo.Update(Payslip);
            //Create Penalty Repo
            var PenaltyRepo = _unitOfWork.GenerateRepository<PayrollPenalty, string>();
            //Map Data
            var MappedData = _mapper.Map<PayrollPenalty>(penaltyToAddDto);
            //Add Penalty
            await PenaltyRepo.AddAsync(MappedData);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Penalty Added Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> EditPenalty(PenaltyToEditDto? penaltyToEditDto)
        {
            //Check On Data
            if (penaltyToEditDto is null) throw new BadRequestException("Invalid Data!");
            //Check On Internal Data
            _ = penaltyToEditDto switch
            {
                { Id: null or ""} => throw new BadRequestException("Invalid Penalty Id!"),
                { Title: null or ""} => throw new BadRequestException("Invalid Penalty Title!"),
                { Value: <= 0} => throw new BadRequestException("Invalid Value!"),
                _ => penaltyToEditDto
            };
            //Create Repo
            var PenaltyRepo = _unitOfWork.GenerateRepository<PayrollPenalty, string>();
            //Create Spec
            var PenaltySpec = new PenaltyById(penaltyToEditDto.Id);
            //Get Penalty
            var Penalty = await PenaltyRepo.GetByIdAsync(PenaltySpec);
            //Check On It
            if (Penalty is null) throw new NotFoundException("Penalty Don't Exist!");
            //Check If Not Pending
            if (Penalty.Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Operate On Approved Or Paid Salary!");
            //Store Prev NetSalary
            var PrevNetySalary = Penalty.Payslip.NetSalary;
            //Store Prev Deduction
            var PrevDeduction = Penalty.Value;
            //Restore The NetSalary
            var RestoredNetSalary = PrevNetySalary + PrevDeduction;
            //Check If Current Value - Net Salary = 0 or less
            if (RestoredNetSalary - penaltyToEditDto.Value < 0) throw new ConflictException("New Penalty Value Exceeded The Salary!");
            //Set The New Deduction 
            Penalty.Payslip.NetSalary = RestoredNetSalary - penaltyToEditDto.Value;
            //Create Payslip Repo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Update Payslip
            PayslipRepo.Update(Penalty.Payslip);
            //Mapping New Penalty
            var mappedPenalty = _mapper.Map(penaltyToEditDto, Penalty);
            //Update New Penalty
            PenaltyRepo.Update(mappedPenalty);
            //Compelete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Penalty Updated Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> DeletePenalty(string? penaltyId)
        {
            //Check On Id
            if (penaltyId is null) throw new BadRequestException("Invalid Id");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<PayrollPenalty, string>();
            //Create Spec
            var Spec = new PenaltyById(penaltyId);
            //Get Penalty
            var Penalty = await Repo.GetByIdAsync(Spec);
            //Check On Penalty
            if (Penalty is null) throw new NotFoundException("Penalty Don't Exist!");
            //Check If Payroll Paid Or Approved
            if (Penalty.Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Operate On Approved Or Paid Salary!");
            //Get CurrentNet
            var currentNetValue = Penalty.Payslip.NetSalary;
            //Get Current Deduction
            var currentDeduction = Penalty.Value;
            //Restore The NetSalary
            var restoredNetSalary = currentNetValue + currentDeduction;
            //Set On Payslip
            Penalty.Payslip.NetSalary = restoredNetSalary;
            //Create Repo For Payslip
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Update Payslip
            PayslipRepo.Update(Penalty.Payslip);
            //Delete Penalty
            Repo.Delete(Penalty);
            //Compelete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Penalty Deleted Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> AddBonus(BonusToAddDto? bonusToAddDto)
        {
            //Check On Data
            if (bonusToAddDto == null) throw new BadRequestException("Invalid Data!");
            //Check On Internal Data
            _ = bonusToAddDto switch
            {
                { PayslipId: "" or null} => throw new BadRequestException("Invalid Payslip Id!"),
                { Title: null or ""} => throw new BadRequestException("Invalid Bonus Title!"),
                { Value: <= 0} => throw new BadRequestException("Invalid Bonus Value!"),
                _ => bonusToAddDto
            };
            //Create PayslipRepo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Payslip Spec
            var PayslipSpec = new PayslipById(bonusToAddDto.PayslipId);
            //Get Payslip
            var Payslip = await PayslipRepo.GetByIdAsync(PayslipSpec);
            //Check On Payslip
            if (Payslip is null) throw new NotFoundException("Payslip Not Exist!");
            //Check If Payslip Not Pending
            if (Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Operate On Approved Or Paid Salary!");
            //Generate New Net Salary
            var newNetSalary = Payslip.NetSalary + bonusToAddDto.Value;
            //Set It Into Payslip
            Payslip.NetSalary = newNetSalary;
            //Update Payslip
            PayslipRepo.Update(Payslip);
            //Create BonusRepo
            var BonusRepo = _unitOfWork.GenerateRepository<PayrollBonus, string>();
            //Mapping Bonus
            var MappedBonus = _mapper.Map<PayrollBonus>(bonusToAddDto);
            //AddBonus
            await BonusRepo.AddAsync(MappedBonus);
            //Compelete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Bonus Added Successfully"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> EditBonus(BonusToEditDto? bonusToEditDto)
        {
            //Check On Data
            if (bonusToEditDto is null) throw new BadRequestException("Invalid Data!");
            //Check On Internal Data
            _ = bonusToEditDto switch
            {
                { Id: null or "" } => throw new BadRequestException("Invalid Penalty Id!"),
                { Title: null or "" } => throw new BadRequestException("Invalid Penalty Title!"),
                { Value: <= 0 } => throw new BadRequestException("Invalid Value!"),
                _ => bonusToEditDto
            };
            //Generate Bonus Repo
            var BonusRepo = _unitOfWork.GenerateRepository<PayrollBonus, string>();
            //generate Bonus Spec
            var BonusSpec = new BonusById(bonusToEditDto.Id);
            //get Bonus
            var Bonus = await BonusRepo.GetByIdAsync(BonusSpec);
            //check On Bonus
            if (Bonus is null) throw new NotFoundException("Bonus Not Exist!");
            //check If Payslip is pending
            if (Bonus.Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Operate On Approved Or Paid Salary!");
            //Get Net Salary
            var restoredNetSalary = Bonus.Payslip.NetSalary - Bonus.Value;
            //New Net Salary
            var newNetSalary = restoredNetSalary + bonusToEditDto.Value;
            //Set New Net Salary
            Bonus.Payslip.NetSalary = newNetSalary;
            //Generate Payslip Repo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Update Payslip
            PayslipRepo.Update(Bonus.Payslip);
            //Mapping DATA
            var mappedData = _mapper.Map(bonusToEditDto, Bonus);
            //Update Bonus
            BonusRepo.Update(mappedData);
            //Compelete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Bonus Updated Successfully"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> DeleteBonus(string? bonusId)
        {
            //Check On Id
            if (bonusId is null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var BonusRepo = _unitOfWork.GenerateRepository<PayrollBonus, string>();
            //Create Spec
            var BonusSpec = new BonusById(bonusId);
            //Get Bonus
            var Bonus = await BonusRepo.GetByIdAsync(BonusSpec);
            //Check On Bonus
            if (Bonus is null) throw new NotFoundException("Bonus Not Exist!");
            //Check if Salary Pending
            if (Bonus.Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Operate On Approved Or Paid Salary!");
            //Restore Net Salary
            var RestoredNetSalary = Bonus.Payslip.NetSalary - Bonus.Value;
            //Set New Net Salary
            Bonus.Payslip.NetSalary = RestoredNetSalary;
            //Create Payslip Repo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Update Payslip
            PayslipRepo.Update(Bonus.Payslip);
            //Delete Bonus
            BonusRepo.Delete(Bonus);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Compelete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Bonus Deleted Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> RePendingApprovedSalary(string? payslipId)
        {
            //Check On Data
            if (payslipId == null) throw new BadRequestException("Innvalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var Spec = new PayslipById(payslipId);
            //Get Payslip
            var Payslip = await Repo.GetByIdAsync(Spec);
            //Check On Payslip
            if (Payslip == null) throw new NotFoundException("Payslip Not Exist!");
            //Check If Paid
            if (Payslip.Status == PayrollStatus.Paid) throw new ConflictException("Can't Restore Paid Salary!");
            //Check If Pending
            if (Payslip.Status == PayrollStatus.Pending) throw new ConflictException("Payslip Already On Pending Status!");
            //Set New Status
            Payslip.Status = PayrollStatus.Pending;
            //Update
            Repo.Update(Payslip);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Compelete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Obj
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Payslip Restored To Pending Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> DeleteSalary(string? payslipId)
        {
            //Check On Id
            if (payslipId == null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var Spec = new PayslipById(payslipId);
            //Get Payslip
            var Payslip = await Repo.GetByIdAsync(Spec);
            //Check On Payslip
            if (Payslip is null) throw new NotFoundException("Payslip Not Exist!");
            //Check If Salary Is Pending
            if (Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Delete An Approved Or Paid Salary!");
            //Remove
            Repo.Delete(Payslip);
            //Compleate
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Salary Deleted Successfully!"
            };
            return Obj;
        }
        public async Task<DataWithPagination<ICollection<PayrollBonusToReturnDto>>> PayslipBonusesGrid(PayrollRelationsParameter parameter)
        {
            //Check On Id
            if (parameter.PayslipId is null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<PayrollBonus, string>();
            //Create Spec
            var Spec = new BonusesByPayslipId(parameter);
            //Get All Bonuses
            var List = await Repo.GetAllAsync(Spec);
            //Mapping
            var MappedList = _mapper.Map<ICollection<PayrollBonusToReturnDto>>(List);
            //Get Count
            var ListCont = await Repo.GetDataCountAsync(Spec);
            //Forming Object
            var Obj = new DataWithPagination<ICollection<PayrollBonusToReturnDto>>(parameter.PageNum, parameter.PageNum + 1, parameter.PageSize, ListCont, MappedList);
            return Obj;
        }
        public async Task<DataWithPagination<ICollection<PayrollPenaltyToReturnDto>>> PayslipPenaltiesGrid(PayrollRelationsParameter parameter)
        {
            //Check On Id
            if (parameter.PayslipId is null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<PayrollPenalty, string>();
            //Create Spec
            var Spec = new PenaltiesByPayslipId(parameter);
            //Get All Bonuses
            var List = await Repo.GetAllAsync(Spec);
            //Mapping
            var MappedList = _mapper.Map<ICollection<PayrollPenaltyToReturnDto>>(List);
            //Get Count
            var ListCont = await Repo.GetDataCountAsync(Spec);
            //Forming Object
            var Obj = new DataWithPagination<ICollection<PayrollPenaltyToReturnDto>>(parameter.PageNum, parameter.PageNum + 1, parameter.PageSize, ListCont, MappedList);
            return Obj;
        }
        public async Task<DataWithPagination<ICollection<PayrollAllowanceToReturnDto>>> PayslipAllowancesGrid(PayrollRelationsParameter parameter)
        {
            //Check On Id
            if (parameter.PayslipId is null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<PayrollAllowance, string>();
            //Create Spec
            var Spec = new AllowancesByPayslipId(parameter);
            //Get All Bonuses
            var List = await Repo.GetAllAsync(Spec);
            //Mapping
            var MappedList = _mapper.Map<ICollection<PayrollAllowanceToReturnDto>>(List);
            //Get Count
            var ListCont = await Repo.GetDataCountAsync(Spec);
            //Forming Object
            var Obj = new DataWithPagination<ICollection<PayrollAllowanceToReturnDto>>(parameter.PageNum, parameter.PageNum + 1, parameter.PageSize, ListCont, MappedList);
            return Obj;
        }
        public async Task<ActionStatusDto> CalculateEmployeesPayrolls() 
        {
            //Get Current Year & Current Month
            var CurrentYear = DateTime.Now.Year;
            var CurrentMonth = DateTime.Now.Month;
            //Get Current Month Days Count
            var MonthDaysCount = DateTime.DaysInMonth(CurrentYear, CurrentMonth);
            //Check If Today Is The Last Day Of The Month
            if (DateTime.Now.Day < MonthDaysCount) throw new BadRequestException("Payrolls Can Only Be Calculated On The Last Day Of The Month!");
            //Create Payslip Repo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Specification
            var PayslipSpec = new PayslipsPerMonthSpeciications();
            //Get Payslips Count For Current Month
            var PayslipsCount = await PayslipRepo.GetDataCountAsync(PayslipSpec);
            //Check If There is Payslip For Current Month
            if(PayslipsCount > 0) throw new ConflictException("Payrolls Already Calculated For Current Month!");
            //Create Employee Repo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Specification
            var EmpSpec = new NotTerminatedOrResignedEmployees();
            //Get All Employees
            var EmpList = await EmpRepo.GetAllAsync(EmpSpec);
            //Vacation Counter
            var VacationCounter = 0;
            //Get Count Of Official Vacation Dayes
            for (int day = 1; day <= MonthDaysCount; day++)
            {
                var Date = new DateTime(CurrentYear, CurrentMonth, day);
                if (Date.DayOfWeek == DayOfWeek.Friday || Date.DayOfWeek == DayOfWeek.Saturday) VacationCounter++;
            }
            //Create List Of Payroll For Add Range
            var PayrollList = new List<AutoPayslipToAddDto>();
            //Looping On Emp List
            foreach( var emp in EmpList )
            {
                //Get Salary Per Month
                var EmpSalaryPerMonth = emp.Salary;
                //Get Salary Per Day
                var EmpSalaryPerDay = EmpSalaryPerMonth.Value / 30;
                //Get Salary Per Hour
                var EmpSalaryPerHour = EmpSalaryPerDay / 8;
                //Get Count Of Fingerprints This Month
                var FingerprintCounts = emp.FingerprintLog.Count();
                //Get Differences Between ActualWorkingDays With FingerprintLog
                var AbsensDayes = (MonthDaysCount - VacationCounter) - FingerprintCounts;
                //Absens Days Can't Be With Negative Value
                if (AbsensDayes < 0) AbsensDayes = 0;
                //BaseDeductedSalary
                decimal DeductedSalary = EmpSalaryPerMonth.Value;
                //Check If Emp Have Approved Vacation Requests
                var VacationRequests = emp.Requests.Where(
                    R =>
                    R.Type == Domain.Entities.Attendance.RequestType.Vacation &&
                    R.Status == Domain.Entities.Attendance.RequestStatus.Approved)
                    .Sum(R => (R.EndDate.DayNumber - R.StartDate.DayNumber) + 1);
                //Check If Vacations > 0 to Deduct It From Absense Day
                if (VacationRequests > 0) AbsensDayes = AbsensDayes - VacationRequests;
                //Get Deductions For Absens
                if (AbsensDayes > 0)
                {
                    DeductedSalary = DeductedSalary - (AbsensDayes * EmpSalaryPerDay * 3);
                }
                //Get Count Of Late Fingerprint
                var LateFingerprintCount = emp.FingerprintLog.Where(FP => 
                FP.Status == Domain.Entities.Attendance.FingerprintStatus.Late).Count();
                //Check On It
                if(LateFingerprintCount > 0)
                {
                    DeductedSalary = DeductedSalary - ((LateFingerprintCount * EmpSalaryPerDay) / 2);
                }
                //Get Delay Fingerprint List
                var DelayFingerprints = emp.FingerprintLog.Where(FP =>
                FP.Status == Domain.Entities.Attendance.FingerprintStatus.Delay).ToList();
                //Check On It
                if(DelayFingerprints.Count > 0)
                {
                    //Get Delay Fingerprint Count
                    var DelayFingerprintCount = DelayFingerprints.Count;
                    //Get Sum Of Duration For Delay Fingerprints
                    var WorkedDuration = DelayFingerprints.Sum(FP => FP.DurationInHours);
                    //Get Supposed Duration With No Delay
                    var SupposedDuration = DelayFingerprintCount * 8;
                    //Get Delay Duration
                    var DelayDurationInMinute = (SupposedDuration - WorkedDuration) * 60;
                    //Get Minute Salary
                    var EmpSalaryPerMinute = EmpSalaryPerHour / 60;
                    //Get Deducted Salary For Delay
                    DeductedSalary = DeductedSalary - (EmpSalaryPerMinute * DelayDurationInMinute.Value);
                }
                //Get Count Of Overtimes
                var TotalOverTime = emp.Requests.Where(
                    R =>
                    R.Type == Domain.Entities.Attendance.RequestType.Overtime &&
                    R.Status == Domain.Entities.Attendance.RequestStatus.Approved)
                    .Sum(E => E.Duration);
                //Check If the Salary Below 0
                if (DeductedSalary < 0) DeductedSalary = 0;
                //Create Object
                var EmployeePayslip = new AutoPayslipToAddDto()
                {
                    BasicSalary = emp.Salary.Value,
                    EmployeeId = emp.Id,
                    EmployeeType = (int)emp.EmployeeType,
                    StartDate = new DateOnly(CurrentYear, CurrentMonth, 1),
                    EndDate = new DateOnly(CurrentYear, CurrentMonth, MonthDaysCount),
                    NetSalary = Math.Round((TotalOverTime.HasValue ? DeductedSalary + (TotalOverTime.Value * EmpSalaryPerHour * 2) : DeductedSalary), 2),
                    TotalOvertime = TotalOverTime
                };
                PayrollList.Add(EmployeePayslip);
            }
            //Mapping Data
            var mappedList = _mapper.Map<ICollection<Payslip>>(PayrollList);
            //Add Range
            await PayslipRepo.AddRangeAsync(mappedList);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Payrolls Added Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> EditEmployeePayslip(PayslipToEditDto? payslipToEditDto)
        {
            //Check On Data
            if(payslipToEditDto is null) throw new BadRequestException("Invalid Data!");
            //Check On Internal Data
            _ = payslipToEditDto switch
            {
                { Id: null or ""} => throw new BadRequestException("Invalid Payslip Id!"),
                { BasicSalary: <= 0} => throw new BadRequestException("Invalid Basic Salary!"),
                { NetSalary: <= 0} => throw new BadRequestException("Invalid Net Salary!"),
                { Status: var s} when !Enum.IsDefined(typeof(PayrollStatus), s) => throw new BadRequestException("Invalid Salary Status!"),
                { PaymentWay: var p} when !Enum.IsDefined(typeof(PayrollPaymentWay), p) => throw new BadRequestException("Invalid Payment Way!"),
                _ => payslipToEditDto
            };
            //Create Repo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var PayslipSpec = new PayslipById(payslipToEditDto.Id);
            //Get Payslip
            var Payslip = await PayslipRepo.GetByIdAsync(PayslipSpec);
            //Check On Payslip
            if (Payslip is null) throw new NotFoundException("Payslip Not Exist!");
            //Check If The Employee Are The Same
            if(Payslip.EmployeeId == payslipToEditDto.EmployeeId) throw new ConflictException("Can't Change The Employee For This Payslip!");
            //Create First Day Of Current Month
            var FirstDayOfCurrentMonth = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
            //Create Last Day Of Current Month
            var LastDayOfCurrentMonth = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            //Check On Period Not Less Than Current Month
            if (Payslip.StartDate < FirstDayOfCurrentMonth || Payslip.EndDate > LastDayOfCurrentMonth) throw new ConflictException("Not Allowed Period, Check StartDate & EndDate");
            //Mapping Data
            var mappedPayslip = _mapper.Map(payslipToEditDto, Payslip);
            //Update Payslip
            PayslipRepo.Update(mappedPayslip);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Salary Edited Successfully!"
            };
            return Obj;
        }
        public async Task<ActionStatusDto> CreateManualPayslip(PayslipToAddDto? payslipToAddDto)
        {
            //Chec On Data
            if (payslipToAddDto is null) throw new BadRequestException("Invalid Data!");
            //Check On Internal Data
            _ = payslipToAddDto switch
            {
                { StartDate: var s, EndDate: var e } when s > e => throw new BadRequestException("Start Date Can't Be Greater Than End Date!"),
                { BasicSalary: <= 0 } => throw new BadRequestException("Invalid Basic Salary!"),
                { NetSalary: <= 0 } => throw new BadRequestException("Invalid Net Salary!"),
                { EmployeeId: null or "" } => throw new BadRequestException("Invalid Employee Id!"),
                _ => payslipToAddDto
            };
            //Create Repo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Spec
            var EmpSpec = new EmployeeByIdSepecification(payslipToAddDto.EmployeeId);
            //Get Employee
            var Employee = await EmpRepo.GetByIdAsync(EmpSpec);
            //Check On Employee
            if (Employee is null) throw new NotFoundException("Employee Not Exist!");
            //Check On The Status Of The Employee
            if(Employee.EmployeeStatus == Domain.Entities.Employee.EmployeeStatus.Terminated || Employee.EmployeeStatus == Domain.Entities.Employee.EmployeeStatus.Resigned)throw new BadRequestException("Can't Create Payslip For Terminated Or Resigned Employee!");
            //Check On Employee Type
            if(Employee.EmployeeType != Domain.Entities.Employee.EmployeeType.Freelance) throw new ConflictException("Manual Payslip Can Only Be Created For Freelance Employees!");
            //Create Repo For Payslip
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec For Payslip
            var PayslipSpec = new PayslipByEmployeeIdAndDateSpecification(payslipToAddDto.EmployeeId, payslipToAddDto.StartDate, payslipToAddDto.EndDate);
            //Get Payslip
            var Payslip = await PayslipRepo.GetDataCountAsync(PayslipSpec);
            //Check If There Is Already Payslip For This Employee In The Same Period
            if (Payslip > 0) throw new ConflictException("There Is Already A Payslip For This Employee In The Same Period, Choose Another Period!");
            //Mapping Data
            var mappedPayslip = _mapper.Map<Payslip>(payslipToAddDto);
            //Add Payslip
            await PayslipRepo.AddAsync(mappedPayslip);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if (!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Salary Created Successfully!"
            };
            return Obj;
        }

        public Task<ActionStatusDto> AddAllowance(AllowanceToAddDto? allowanceToAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<ActionStatusDto> EditAllownace(AllowanceToEditDto? allowanceToEditDto)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionStatusDto> DeleteAllowance(string? allowanceId)
        {
            //Check On Id
            if(allowanceId is null) throw new BadRequestException("Invalid Id!");
            //Create Repo
            var Repo = _unitOfWork.GenerateRepository<PayrollAllowance, string>();
            //Create Spec
            var Spec = new AllowanceByIdSpecification(allowanceId);
            //Get Allowance
            var Allowance = await Repo.GetByIdAsync(Spec);
            //Check On Allowance
            if(Allowance is null) throw new NotFoundException("Allowance Not Exist!");
            //Check If Payslip Is Not Pending
            if(Allowance.Payslip.Status != PayrollStatus.Pending) throw new ConflictException("Can't Operate On Approved Or Paid Salary!");
            //Restore Net Salary
            Allowance.Payslip.NetSalary = Allowance.Payslip.NetSalary - Allowance.Value;
            //Create Payslip Repo
            var PayslipRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Update Payslip
            PayslipRepo.Update(Allowance.Payslip);
            //Remove Allowance
            Repo.Delete(Allowance);
            //Complete
            var Complete = await _unitOfWork.CompleteAsync() > 0;
            //Check On Complete
            if(!Complete) throw new Exception("Something Went Wrong!");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Allowance Deleted Successfully!"
            };
            return Obj;
        }
    }
}
