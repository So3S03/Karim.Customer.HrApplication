namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public class EmployeeQueryParameters
    {
        private const int maxPageSize = 10;
        private const int minPageSize = 5;
        private int pageNum = 1;
        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value <= 0 ? 1 : pageNum; }
        }
        private int pageSize = minPageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value < minPageSize || value == 0 ? minPageSize : (value > maxPageSize ? maxPageSize : value); }
        }

        public int? Sorting { get; set; }
        public int? WorkType { get; set; }
        public int? EmployeeType { get; set; }
        public string? Name { get; set; }

        private int? contractChecker;

        public int? ContractChecker
        {
            get { return contractChecker; }
            set { contractChecker = value > 2 ? 2 : (value <= 0 ? 1 : value); }
        }

        public int? EmployeeStatus { get; set; }
        public string? Department { get; set; }

    }
}
