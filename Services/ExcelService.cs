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
                        Details = new ProductDetail
                        {
                            ProductDescription = row.Cell(6).GetString(),
                            Camera = row.Cell(7).GetString(),
                            StorageCapacity = row.Cell(8).GetString(),
                            FastCharging = row.Cell(9).GetString(),
                            FaceID = row.Cell(10).GetString(),
                            Technology = row.Cell(11).GetString(),
                            Style = row.Cell(12).GetString(),
                            Size = row.Cell(13).GetString(),
                            Model = row.Cell(14).GetString(),
                            Colour = row.Cell(15).GetString(),
                            Weight = row.Cell(16).GetString(),
                            Video = row.Cell(17).GetString(),
                            CameraTrueDepth = row.Cell(18).GetString(),
                            ChargingConnectivity = row.Cell(19).GetString(),
                            Battery = row.Cell(20).GetString(),
                            Country = row.Cell(21).GetString(),
                            Company = row.Cell(22).GetString(),
                            Guarantee = row.Cell(23).GetString(),
                            WaterResistant = row.Cell(24).GetString(),
                            CameraFeatures = row.Cell(25).GetString(),
                            GPU = row.Cell(26).GetString(),
                            Pin = row.Cell(27).GetString(),
                            ChargingSupport = row.Cell(28).GetString(),
                            NetworkSupport = row.Cell(29).GetString(),
                            WiFi = row.Cell(30).GetString(),
                            Bluetooth = row.Cell(31).GetString(),
                            GPS = row.Cell(32).GetString(),
                            ChargingTechnology = row.Cell(33).GetString(),
                            FingerprintSensor = row.Cell(34).GetString(),
                            SpecialFeatures = row.Cell(35).GetString(),
                            RearCamera = row.Cell(36).GetString(),
                            FrontCamera = row.Cell(37).GetString(),
                            SIM = row.Cell(38).GetString(),
                            Sensor = row.Cell(39).GetString(),
                            Ram = row.Cell(40).GetString(),
                            CPU = row.Cell(41).GetString(),
                            NFC = row.Cell(42).GetString(),
                            Chip = row.Cell(43).GetString(),
                            ScreenResolution = row.Cell(44).GetString(),
                            ScreenFeatures = row.Cell(45).GetString(),
                            InternalMemory = row.Cell(46).GetString(),
                            BatteryCapacity = row.Cell(47).GetString(),
                            ScreenSize = row.Cell(48).GetString(),
                            Screen = row.Cell(49).GetString(),
                            OperatingSystem = row.Cell(50).GetString(),
                        }
                    };

                    products.Add(product);
                }
            }

            return products;
        }
    }
}
