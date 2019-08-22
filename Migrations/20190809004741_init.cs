using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wahama.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            
            migrationBuilder.AddColumn<string>(
    name: "ShortDescription",
    table: "Product",
    nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
