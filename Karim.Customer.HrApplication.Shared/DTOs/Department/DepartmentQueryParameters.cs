using System.Drawing;

namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public class DepartmentQueryParameters
    {
        public int? Type { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; } = 0;
        public int? Sorting { get; set; }

        private int pageNum = 1;

        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value <= 0 ? 1 : value; }
        }

        private const int size = 6;
        private const int maxSize = 9;
        private int pageSize = 6;

        public int PageSize
        {
            get { return pageSize; }
            set 
            {
                if(value <= 0)
                    pageSize = size;
                else if (value > maxSize)
                    pageSize = maxSize;
                else 
                    pageSize = value;
            }
        }

    }
}
