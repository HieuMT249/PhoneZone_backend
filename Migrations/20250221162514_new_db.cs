using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phonezone_backend.Migrations
{
    public partial class new_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                collation: "Vietnamese_CI_AS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Orders");
        }
    }
}
