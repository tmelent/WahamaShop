using Microsoft.EntityFrameworkCore.Migrations;

namespace Wahama.Migrations
{
    public partial class close1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShoppingCart_ShoppingCartId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShoppingCartId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
