using Karim.Customer.HrApplication.Application._Common.DateConverter;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance;
using Karim.Customer.HrApplication.Application.Specifications.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using System.ComponentModel;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Application._Common.EnumConverter;

namespace Karim.Customer.HrApplication.Application.Services.Attendance
{
    internal class AttendanceServices(IUnitOfWork _unitOfWork, IMapper mapper) : IAttendanceServices
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
