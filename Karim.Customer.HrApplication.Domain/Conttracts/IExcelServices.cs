namespace Karim.Customer.HrApplication.Domain.Conttracts
{
    public interface IExcelServices
    {
        byte[] GenerateExcelSheetTemplate<T>(T Example, string sheetName);
        byte[] GenerateExcelSheetForCollection<T>(IEnumerable<T> entityList, string sheetName);
        //Task<ICollection<T>> ReadExcelSheet<T>(Stream file);
    }
}
