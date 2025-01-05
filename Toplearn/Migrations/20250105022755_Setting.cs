using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class Setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearsOfExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfCustomers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfEmployees = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentID", "PermissionTitle" },
                values: new object[] { 28, null, "Setting" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[] { 28, 28, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 28);
        }
    }
}
