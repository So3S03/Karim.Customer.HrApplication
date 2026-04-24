namespace Karim.Customer.HrApplication.Shared.DTOs.Contracts
{
    public class ContractParameters
    {
		private int type;
		public required int Type
		{
			get { return type; }
			set { type = value <= 0 ? 1 : (value > 2 ? 2 : value); }
		}
		private int? status;
		public int? Status
		{
			get { return status; }
			set { status = value <= 0 ? 1 : (value > 5 ? 5 : value); }
		}
		
		private const int maxPageSize = 10;
		private const int minPageSize = 5;
		private int pageSize;

		public required int PageSize
		{
			get { return pageSize; }
			set { pageSize = value < minPageSize ? minPageSize : (value > maxPageSize ? maxPageSize : value); }
		}

		private const int minPageNum = 1;
		private int pageNum;

		public required int PageNum
		{
			get { return pageNum; }
			set { pageNum = value < minPageSize ? minPageSize : value; }
		}


	}
}
