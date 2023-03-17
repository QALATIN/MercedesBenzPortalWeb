using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace MercedesBenzApiReportes.Helpers
{
    public class ExcelHelper
    {
        public byte[] CrearExcel(string nombreReporte, DataTable data)
        {
            using XLWorkbook wb = new();
            wb.Properties.Title = $"Reporte {nombreReporte}";
            wb.Properties.Author = "Latin Id";
            wb.Properties.Subject = $"Reporte {nombreReporte}";
            wb.Properties.Keywords = "MercedesBenz, LatinId, Reporte";

            wb.Worksheets.Add(data, nombreReporte);

            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }

    }
}
