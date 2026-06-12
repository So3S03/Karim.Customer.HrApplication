using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Payrolls;
using Karim.Customer.HrApplication.Application.Specifications.Payrolls;
using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Payroll;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;

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
    }
}
