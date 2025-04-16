using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SalesOrderApi.DTO.SaleOrder.Response;

namespace SalesOrderApi.Helpers
{
    public static class ExcelHelper 
    {
        public static void CreateHeader (List<string> Name, ISheet sheet, XSSFWorkbook workbook)
        {
            IFont font = workbook.CreateFont();
            font.IsBold = true;

            ICellStyle style = workbook.CreateCellStyle();
            style.SetFont(font);
            IRow header = sheet.CreateRow(0);

            for (int i = 0; i < Name.Count; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(Name[i]);
                cell.CellStyle = style;
            }

        }
        public static string ExportExcel(List<SearchResponse> data)
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1"); 

            var header = new List<string> { "ORDER NO", "ORDER DATE", "CUSTOMER NAME" };
            CreateHeader(header, sheet, workbook);

            int rowNum = 1;
            foreach(var so in data)
            {
                var row = sheet.CreateRow(rowNum);
                var col =0;

                row.CreateCell(col++).SetCellValue(so.OrderNo);
                row.CreateCell(col++).SetCellValue(so.OrderDate);
                row.CreateCell(col++).SetCellValue(so.CustomerName);
                rowNum++;
            }

            using (var memoryStream = new MemoryStream())
            {
                workbook.Write(memoryStream);
                var bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            } ;
        }
      
    }
}
