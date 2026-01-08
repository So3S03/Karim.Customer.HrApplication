using Microsoft.AspNetCore.Http;

namespace Karim.Customer.HrApplication.Domain.Conttracts
{
    public interface IExcelServices
    {
        byte[] GenerateExcelSheetTemplate<T>(T Example, string sheetName);
        byte[] GenerateExcelSheetForCollection<T>(IEnumerable<T> entityList, string sheetName);
        List<T> ReadExcelSheetForCollections<T>(IFormFile? file) where T : new();
    }
}
