using Microsoft.EntityFrameworkCore.Migrations;

namespace itfco.Web.Migrations
{
    public partial class TBLProductColDrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadLines",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Products",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Products",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadLines",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Products",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }
    }
}
