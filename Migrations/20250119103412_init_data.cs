using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phonezone_backend.Migrations
{
    public partial class init_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Camera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FastCharging = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaceID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Technology = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CameraTrueDepth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargingConnectivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Battery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guarantee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaterResistant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CameraFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargingSupport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkSupport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WiFi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bluetooth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargingTechnology = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FingerprintSensor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RearCamera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontCamera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SIM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sensor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenResolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalMemory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatteryCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Screen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
