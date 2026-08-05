namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class PayrollParameter
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int Status { get; set; }
        public int PaymentWay { get; set; }

        private int pageNum;
        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value <= 0 ? 1 : value; }
        }

        private const int minPageSize = 5;
        private const int maxPageSize = 10;
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value < minPageSize ? minPageSize : (value > maxPageSize ? maxPageSize : value); }
        }


    }
}
