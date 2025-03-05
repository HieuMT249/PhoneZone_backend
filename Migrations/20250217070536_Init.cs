using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phonezone_backend.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    UsageLimit = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    OldPrice = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    NewPrice = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Stock = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS")
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Thumbnails = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    OutstandingFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Camera = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    CameraTrueDepth = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    ChargingConnectivity = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Battery = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Guarantee = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    WaterResistant = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    CameraFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    GPU = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    ChargingSupport = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    NetworkSupport = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    WiFi = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Bluetooth = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    GPS = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    ChargingTechnology = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    FingerprintSensor = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    SpecialFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    RearCamera = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    FrontCamera = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    SIM = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Sensor = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Ram = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    CPU = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    NFC = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Chip = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    ScreenResolution = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    ScreenFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    InternalMemory = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    BatteryCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    ScreenSize = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Screen = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    DiscountAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    FinalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishList_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersCoupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "Vietnamese_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersCoupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersCoupons_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersCoupons_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WishListId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => new { x.WishListId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WishListItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItems_WishList_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersCoupons_CouponId",
                table: "OrdersCoupons",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersCoupons_OrderId",
                table: "OrdersCoupons",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_ProductId",
                table: "Specifications",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishList_UserId",
                table: "WishList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrdersCoupons");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
