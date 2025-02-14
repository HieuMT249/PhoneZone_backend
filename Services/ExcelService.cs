using ClosedXML.Excel;
using phonezone_backend.Models;
using System.Collections.Generic;

namespace phonezone_backend.Services
{
    public class ExcelService
    {
        public List<Product> ReadDataFromExcel(string filePath)
        {
            var products = new List<Product>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();

                foreach (var row in rows.Skip(1))
                {
                    var product = new Product
                    {
                        Branch = row.Cell(1).GetString(),
                        ProductName = row.Cell(2).GetString(),
                        NewPrice = row.Cell(3).GetString(),
                        OldPrice = row.Cell(4).GetString(),
                        Image = row.Cell(5).GetString(),
                        ProductDescription = row.Cell(6).GetString(),
                        Specification = new Specification
                        {
                            Camera = row.Cell(7).GetString(),
                            Model = row.Cell(8).GetString(),
                            Colour = row.Cell(9).GetString(),
                            Weight = row.Cell(10).GetString(),
                            Video = row.Cell(11).GetString(),
                            CameraTrueDepth = row.Cell(12).GetString(),
                            ChargingConnectivity = row.Cell(13).GetString(),
                            Battery = row.Cell(14).GetString(),
                            Country = row.Cell(15).GetString(),
                            Company = row.Cell(16).GetString(),
                            Guarantee = row.Cell(17).GetString(),
                            WaterResistant = row.Cell(18).GetString(),
                            CameraFeatures = row.Cell(19).GetString(),
                            GPU = row.Cell(20).GetString(),
                            Pin = row.Cell(21).GetString(),
                            ChargingSupport = row.Cell(22).GetString(),
                            NetworkSupport = row.Cell(23).GetString(),
                            WiFi = row.Cell(24).GetString(),
                            Bluetooth = row.Cell(25).GetString(),
                            GPS = row.Cell(26).GetString(),
                            ChargingTechnology = row.Cell(27).GetString(),
                            FingerprintSensor = row.Cell(28).GetString(),
                            SpecialFeatures = row.Cell(29).GetString(),
                            RearCamera = row.Cell(30).GetString(),
                            FrontCamera = row.Cell(31).GetString(),
                            SIM = row.Cell(32).GetString(),
                            Sensor = row.Cell(33).GetString(),
                            Ram = row.Cell(34).GetString(),
                            CPU = row.Cell(35).GetString(),
                            NFC = row.Cell(36).GetString(),
                            Chip = row.Cell(37).GetString(),
                            ScreenResolution = row.Cell(38).GetString(),
                            ScreenFeatures = row.Cell(39).GetString(),
                            InternalMemory = row.Cell(40).GetString(),
                            BatteryCapacity = row.Cell(41).GetString(),
                            ScreenSize = row.Cell(42).GetString(),
                            Screen = row.Cell(43).GetString(),
                            OperatingSystem = row.Cell(44).GetString(),
                            OutstandingFeatures = row.Cell(45).GetString(),
                            Thumbnails = row.Cell(46).GetString(),

                        }
                    };

                    products.Add(product);
                }
            }

            return products;
        }
    }
}
