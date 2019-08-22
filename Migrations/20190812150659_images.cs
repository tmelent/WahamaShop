using Microsoft.EntityFrameworkCore.Migrations;

namespace Wahama.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "ProductSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "ProductFraction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "ProductAlliance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "ProductFraction");

            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "ProductAlliance");
        }
    }
}
