using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wahama.Migrations
{
    public partial class accounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ShoppingCart",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "moderator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "employee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCart",
                nullable: false,
                oldClrType: typeof(int));

            
        }
    }
}
