namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class RequestsParameters
    {
        public required string EmpId { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }

        private DateOnly? startDate;

        public DateOnly? StartDate
        {
            get { return startDate; }
            set { startDate = value.HasValue == false ? new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1) : value.Value; }
        }

        private DateOnly? endDate;

        public DateOnly? EndDate
        {
            get { return endDate; }
            set { endDate = value.HasValue == false ? new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) : value.Value; }
        }

        private const int maxPageSize = 10;
        private const int minPageSize = 5;
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : (value < minPageSize ? minPageSize : value); }
        }

        private int pageNum;

        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value < 1 ? 1 : value; }
        }

    }
}
