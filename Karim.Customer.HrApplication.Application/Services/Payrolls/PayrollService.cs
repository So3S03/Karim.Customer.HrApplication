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
    }
}
