namespace Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs
{
    public class ActionStatusDto<TEntity> where TEntity : class
    {
        public bool Status { get; set; }
        public required string Message { get; set; }
        public ICollection<TEntity>? Data { get; set; } //It Could return with null if the adding/updating process went wrong
    }
}
