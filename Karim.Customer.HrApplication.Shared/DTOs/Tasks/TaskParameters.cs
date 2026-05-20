namespace Karim.Customer.HrApplication.Shared.DTOs.Tasks
{
    public class TaskParameters
    {
        public string? Name { get; set; }
		private int type;

		public int Type
		{
			get { return type; }
			set { type = value > 2 ? 2 : (value <= 0 ? 1 : value); }
		}

		private int status;

		public int Status
		{
			get { return status; }
			set { status = value > 4 ? 4 : (value <= 0 ? 1 : value); }
		}

        public string? ProjectId { get; set; }
        public string? TicketId { get; set; }
        public required string EmployeeId { get; set; }

		private int pageNum;

		public int PageNumber
		{
			get { return pageNum; }
			set { pageNum = value <=0 ? 1 : value; }
		}

		private int pageSize;
		private const int maxPageSize = 10;
		private const int minPageSize = 5;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > maxPageSize ? maxPageSize : (value < minPageSize ? minPageSize : value); }
		}


	}
}
