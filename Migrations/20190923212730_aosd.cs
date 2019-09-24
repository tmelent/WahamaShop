using Microsoft.EntityFrameworkCore.Migrations;

namespace Wahama.Migrations
{
    public partial class aosd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                            name: "SellerId",
                            table: "Check",
                            nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Check_SellerId",
                table: "Check",
                column: "SellerId");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
