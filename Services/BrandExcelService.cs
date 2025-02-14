using ClosedXML.Excel;
using phonezone_backend.Models;

namespace phonezone_backend.Services
{
    public class BrandExcelService
    {
        public List<Brand> ReadDataFromExcel(string filePath)
        {
            var brands = new List<Brand>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();

                foreach (var row in rows.Skip(1))
                {
                    var brand = new Brand
                    {
                        Code = row.Cell(1).GetString(),
                        Name = row.Cell(2).GetString(),
                        Country = row.Cell(3).GetString(),
                        Image = row.Cell(4).GetString(),
                    };

                    brands.Add(brand);
                }
            }

            return brands;
        }
    }
}
