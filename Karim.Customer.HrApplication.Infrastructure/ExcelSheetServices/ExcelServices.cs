using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using Karim.Customer.HrApplication.Domain.Conttracts;

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
            //15. converting to bytes
            //a. forming object of streaming to save the file on it
            using var stream = new MemoryStream();
            //b. save workbook in stream
            workBook.SaveAs(stream);
            //c. returning the bytes from stream
            return stream.ToArray(); 
        }

        //public Task<ICollection<T>> ReadExcelSheet<T>(Stream file)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
