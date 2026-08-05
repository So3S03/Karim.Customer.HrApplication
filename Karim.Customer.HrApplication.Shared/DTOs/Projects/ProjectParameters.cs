namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public class ProjectParameters
    {
        public string? Name { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string? Department { get; set; }
        private int pageNum;

        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value <= 0 ? 1 : value; }
        }

        private const int maxPageSize = 10;
        private const int minPageSize = 5;
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > maxPageSize ? maxPageSize : (value < minPageSize ? minPageSize : value)); }
        }


    }
}
