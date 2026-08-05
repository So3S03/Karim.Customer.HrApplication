using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Application._Common.EnumConverter
{
    internal static class EnumsConvertion
    {
        //Method For Converting the Enums into List For Fill Enums APIs
        public static ICollection<EnumDto> CreateEnumLists<TEnum>()
            where TEnum : Enum
        {
            var convertedData = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new EnumDto() { Value = Convert.ToInt32(e), Name = e.ToString(), DisplayedName = CusomizeEnumName(e) })
                .ToList();
            return convertedData;
        }
        public static string CusomizeEnumName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                      .FirstOrDefault() as DisplayAttribute;
            return attr?.Name ?? value.ToString();
        }
    }
}
