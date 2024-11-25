using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itfco.Web.Migrations
{
    public partial class TBLProductColTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Products");

         
        }
    }
}
