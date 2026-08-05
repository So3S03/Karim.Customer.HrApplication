using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Spreadsheet;
using Karim.Customer.HrApplication.Domain.Conttracts;
using Karim.Customer.HrApplication.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Karim.Customer.HrApplication.Infrastructure.ExcelSheetServices
{
    public class ExcelServices : IExcelServices
    {
        public byte[] GenerateExcelSheetTemplate<T>(T Example, string sheetName)
        {
            //1. create workbook (its container for worksheets [like many excel tables tabs])
            using var workBook = new XLWorkbook();
            //2. create worksheet (it's the one which hold the the single table)
            var workSheet = workBook.Worksheets.Add(sheetName);
            //3. create columns based on dataType
            var columnsName = typeof(T).GetProperties();
            //4. create headLine
            for(int i = 0; i < columnsName.Length; i++)
            {
                //5. holding a cell to make an operation on it
                var FirstRowCell = workSheet.Cell(1/*row num*/ , i + 1/*column number*/); //it takes 2 params first => selecting the row number , second => determine number of columns that i will use
                //6. give the cell an value
                FirstRowCell.Value = columnsName[i].Name;//giving the cell an value like Name, Age, Date ....etc
                //7. style the cell
                FirstRowCell.Style.Font.Bold = true;
                FirstRowCell.Style.Font.Italic = true;
                FirstRowCell.Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
                FirstRowCell.Style.Border.OutsideBorder = XLBorderStyleValues.Double;
                //8. hold the second row to append example data
                var SecRowCell = workSheet.Cell(2 , i + 1 );
                //append data on cell if the column data is not null
                if(columnsName[i].GetValue(Example) is not null) SecRowCell.Value = XLCellValue.FromObject(columnsName[i].GetValue(Example));
            }
            //Text Aligning
            workSheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //9. style all columns width to be fit with contetnt
            workSheet.Columns().AdjustToContents();
            //10. create MemoryStream object for download the file
            using var stream = new MemoryStream();
            //11. convert sheet into byte[] for download
            workBook.SaveAs(stream);
            return stream.ToArray();

        }

        public byte[] GenerateExcelSheetForCollection<T>(IEnumerable<T> entityList, string sheetName)
        {
            //1. create workBook
            using var workBook = new XLWorkbook();
            //2. create workSheet
            var workSheet = workBook.Worksheets.Add(sheetName);
            //3. create headers name
            var columnsName = typeof(T).GetProperties();
            //4. create first row (header) with styling
            for(int i = 0; i < columnsName.Length; i++)
            {
                //5. hold first row cell by cell
                var cell = workSheet.Cell(1, i + 1);
                //6. put the headers names on each cell
                cell.Value = columnsName[i].Name;
                //7. stylling the header
                cell.Style.Font.Bold = true;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                cell.Style.Font.Italic = true;
                cell.Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Double;
            }
            //8. patch the data into the work sheet
            int rowIndex = 2;
            foreach (var item in entityList)
            {
                for (int j = 0; j < columnsName.Length; j++)
                {
                    //9. hold second row tell the end of the list
                    var cell = workSheet.Cell(rowIndex,  j + 1);
                    //10. store value for checking
                    var value = columnsName[j].GetValue(item);
                    //11. check on value
                    if (value != null)
                    {
                        //12. insert value into cell
                        cell.Value = XLCellValue.FromObject(value);
                    }
                }
                //13. increase the row number
                rowIndex++;
            }
            //14. adjust all cells width
            workSheet.Columns().AdjustToContents();
            //Text Aligning
            workSheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //15. converting to bytes
            //a. forming object of streaming to save the file on it
            using var stream = new MemoryStream();
            //b. save workbook in stream
            workBook.SaveAs(stream);
            //c. returning the bytes from stream
            return stream.ToArray(); 
        }

        public HashSet<T> ReadExcelSheetForCollections<T>(IFormFile? file) where T : new()
        {
            //1. Check if the file exist
            if (file == null || file.Length == 0) throw new BadRequestException("File Not Found");

            //2. Check file extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            var validExtensions = new[] { ".xlsx", ".xls" };
            if (!validExtensions.Contains(extension))
                throw new BadRequestException("File Type You Have Provided Is Not Valid Please Provide File Of Type XLSX or XLS");

            //3. Create stream for streaming file
            using var streamedFile = file.OpenReadStream();

            //4. Create Collection Var For Pushing The Records On It
            var entityList = new HashSet<T>();

            //5. Open Excel File as WorkBook
            using var workBook = new XLWorkbook(streamedFile);

            //6. Get First Work Sheet
            var workSheet = workBook.Worksheet(1);

            //7. Get header row and map to properties
            var headerRow = workSheet.Row(1);

            //8. Get All Columns Name
            var colNamesList = headerRow.Cells()
                .Select(c => c.GetValue<string>().Trim().ToLower())
                .ToList();

            //9. Check If There is Any Column Names
            if (!colNamesList.Any())
                throw new BadRequestException("File Doesn't Contain Any Column Names");

            //10. Get All Properties From T and Store Them Into Dictionary
            var properties = typeof(T).GetProperties()
                .ToDictionary(p => p.Name.ToLower(), p => p);

            //11. Loop in Rows
            foreach (var row in workSheet.RowsUsed().Skip(1))
            {
                //12. Create an item to push it into list
                var item = new T();

                //13. Loop On Cells
                for (int i = 0; i < colNamesList.Count; i++)
                {
                    //14. Holding Current ColumnName
                    var singleColName = colNamesList[i];

                    //15. Check if The Column Exist
                    if (!properties.TryGetValue(singleColName, out var property))
                        //16. Ignoring The Column
                        continue;

                    //17. Holding The Cell To Get The Value From it
                    var cell = row.Cell(i + 1);

                    //18. Check If The Cell Has Value Or Not
                    if (cell.IsEmpty())
                        //19. Ignore The Cell
                        continue;

                    //20. Getting Cell Type
                    var targetType = Nullable.GetUnderlyingType(property.PropertyType)
                                     ?? property.PropertyType;

                    //21. Get value based on type
                    object? value = targetType.Name switch
                    {
                        nameof(String) => cell.GetValue<string>(),
                        nameof(Int32) => cell.GetValue<int>(),
                        nameof(Int64) => cell.GetValue<long>(),
                        nameof(Decimal) => cell.GetValue<decimal>(),
                        nameof(Double) => cell.GetValue<double>(),
                        nameof(DateTime) => cell.GetValue<DateTime>(),
                        nameof(TimeOnly) => TimeOnly.FromDateTime(DateTime.Parse(cell.GetValue<string>())),
                        nameof(DateOnly) => DateOnly.FromDateTime(cell.GetValue<DateTime>()),
                        nameof(Boolean) => cell.GetValue<bool>(),
                        _ => Convert.ChangeType(cell.Value, targetType)
                    };

                    //22. Setting Value Into Property
                    property.SetValue(item, value);
                }

                //23. Adding The Created Entity Into my List To Return It
                entityList.Add(item);
            }

            //24. Return The List
            return entityList;
        }
    }
}
