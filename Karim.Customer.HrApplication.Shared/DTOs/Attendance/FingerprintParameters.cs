namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class FingerprintParameters
    {
        const int maxPageSize = 10;
        const int minPageSize = 5;
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }

        private int? status;

        public int? Status
        {
            get { return status; }
            set { status = value > 5 ? 5 : (value < 1 ? 1 : value); }
        }

        private int pageNum;

        public required int PageNum
        {
            get { return pageNum; }
            set { pageNum = value < 1 ? 1 : value; }
        }

        private int pageSize;

        public required int PageSize
        {
            get { return pageSize; }
            set { pageSize = value < 5 ? minPageSize : (value > 10 ? maxPageSize : value); }
        }
        public string? Name { get; set; }
        public string? EmpId { get; set; }
    }
}
