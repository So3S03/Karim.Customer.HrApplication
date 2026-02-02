using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using MapsterMapper;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using System.Text.RegularExpressions;
using Karim.Customer.HrApplication.Shared.Exceptions;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;
using System.ComponentModel;
using Karim.Customer.HrApplication.Application._Common.EnumConverter;

namespace Karim.Customer.HrApplication.Application.Services.Employee
{
    internal class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeService
    {
        private const string codePattern = @"^EMP\d{3,}$";
        public async Task<MaxCodeResult> GenerateEmployeeMaxCode()
        {
            //Create Repo 
            var Repo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Specificatiom
            var spec = new EmployeeByCodeSortingDesc();
            //Get First Row 
            var Employee = await Repo.GetByIdAsync(spec);
            //Create Inintiate Variable For Code
            string Code = "";
            //Check On Employee 
            if (Employee is null) Code = "EMP001";
            else
            {
                //Create Variable For Saving Last Employee Code
                string extractedCode = Employee.EmployeeCode;
                //Check on This Code
                if (extractedCode == null || Regex.IsMatch(extractedCode, codePattern)) throw new BadRequestException("Last Code Entered Is In Wrong Format!");
                //Get Numeric Value For Claculation
                int.TryParse(extractedCode.Split("P")[1], out int numaricValue);
                //Increment Numeric Value
                numaricValue = numaricValue + 1;
                //Form New Code
                Code = $"EMP{numaricValue.ToString().PadLeft(3, '0')}";
            }
            //Form Object To Return New Code
            var Obj = new MaxCodeResult()
            {
                MaxCode = Code
            };
            //Return Code
            return Obj;
        }
        public ICollection<EnumDto> EmployeeSortingLockup()
        {
            //Create Array Of The Converted Enum
            var EmployeeSortingLockups = EnumsConvertion.CreateEnumLists<EmployeeSortingLockup>();
            //Return Collection
            return EmployeeSortingLockups;
        }
        public async Task<DataWithPagination<ICollection<EmployeeToReturnDto>>> GetAllEmployeeWithPagination(EmployeeQueryParameters? parameters)
        {
            //Get Employee List
            IEnumerable<employee> employees = await getAllEmployees(parameters);
            //Converting List Into EmployeeDto
            var mappedEmployees = _mapper.Map<ICollection<EmployeeToReturnDto>>(employees);
            //Forming Paginated Object
            var obj = new DataWithPagination<ICollection<EmployeeToReturnDto>>(1, 2, 5, 100, mappedEmployees);
            //return object 
            return obj;
        }

        //Helper Methods
        private async Task<IEnumerable<employee>> getAllEmployees(EmployeeQueryParameters? parameters)
        {
            //Create Specification Object
            EmployeeListSpecification? spec = null;
            //Check On Specifications
            if (parameters is null) spec = null;
            //Create Repo
            var repo = _unitOfWork.GenerateRepository<employee, string>();
            //Get All Employees
            var employeeList = await repo.GetAllAsync(spec!);
            //return the result
            return employeeList;
        }
    }
}
