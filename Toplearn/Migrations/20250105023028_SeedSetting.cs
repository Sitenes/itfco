using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class SeedSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "NumberOfCustomers", "NumberOfEmployees", "YearsOfExperience" },
                values: new object[] { 1, "100", "12", "14" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
