namespace Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs
{
    public class DataWithPagination<T>
    {
        public decimal PageNumber { get; set; }
        public decimal NextPageNumber { get; set; }
        public decimal PageSize { get; set; }
        public decimal NumberOfPages { get; }
        public decimal TotalRecords { get; set; }
        public T? Data { get; set; }
        public DataWithPagination(decimal pageNum, decimal nextPage, decimal pageSize, decimal totalRecords, T data)
        {
            PageNumber = pageNum;
            NumberOfPages = Math.Ceiling(totalRecords / (pageSize != 0 ? pageSize : 1));
            NextPageNumber = nextPage > NumberOfPages ? NumberOfPages : nextPage;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Data = data;
        }
    }
}
