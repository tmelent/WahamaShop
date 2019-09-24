using Microsoft.EntityFrameworkCore.Migrations;

namespace Wahama.Migrations
{
    public partial class po : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShoppingCart",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Order",
                nullable: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShoppingCart_ShoppingCartId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            

        }
    }
}
