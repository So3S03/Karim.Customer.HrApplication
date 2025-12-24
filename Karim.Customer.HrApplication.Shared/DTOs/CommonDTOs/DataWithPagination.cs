namespace Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs
{
    public class DataWithPagination<T>
    {
        public decimal PageNumber { get; set; }
        public decimal NextPageNumber { get; set; }
        public decimal PageSize { get; set; }
        public decimal NumberOfPages { get; set; }
        public decimal TotalRecords { get; set; }
        public ICollection<T>? Data { get; set; }
    }
}
