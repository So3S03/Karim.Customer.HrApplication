namespace Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs
{
    public class FillEntityDto<T> where T : IEquatable<T>
    {
        public required T Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
    }
}
