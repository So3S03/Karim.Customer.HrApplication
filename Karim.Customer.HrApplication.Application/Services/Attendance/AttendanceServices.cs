using Karim.Customer.HrApplication.Application._Common.DateConverter;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance;
using Karim.Customer.HrApplication.Application.Specifications.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using System.ComponentModel;

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
            //Forming Repo
            var Repo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Forming Specification
            var Spec = new TodaysFingerprintByEmpIdSpecification(EmpId, Today);
            //Get Fingerprint
            var fingerprint = await Repo.GetByIdAsync(Spec);
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
                    Lat = null,
                    Long = null,
                    Status = FingerprintSatusLockup.Absense.ToString()
                };
                return emptyObject;
            }
            //Foming Mapped Fingerprint
            var mappedFinger = mapper.Map<SpecificFingerprintToReturnDto>(fingerprint);
            return mappedFinger;
        }
        //public Task<ActionStatusDto> InsertFingerprint(FingerprintToBeInsertDto? fingerprint)
        //{
        //    //Check On Data
        //    if (fingerprint == null) throw new BadRequestException("Invalid Data, Please Try Again Later");
        //    //Get Today Date
        //    var today = DateTime.UtcNow;

        //}
    }
}
