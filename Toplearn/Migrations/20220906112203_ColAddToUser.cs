using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class ColAddToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("00ac29bb-ea32-4fb2-b4b2-58291dc459b7"));

            migrationBuilder.AddColumn<bool>(
                name: "IsTeacher",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "EmailLink", "ExpireEmailLink", "IsActive", "IsDeleted", "IsTeacher", "Password", "Phone", "RegisterDate", "UserAvatar", "UserName", "Wallet" },
                values: new object[] { new Guid("c2f3eef3-5907-4452-9888-f30aa31feabc"), "", new Guid("50ab32c1-c41b-4257-9566-02d85b48d7d5"), new DateTime(2022, 9, 6, 15, 52, 2, 889, DateTimeKind.Local).AddTicks(1033), true, false, false, "A6-6A-BB-56-84-C4-59-62-D8-87-56-4F-08-34-6E-8D", 0L, new DateTime(2022, 9, 6, 15, 52, 2, 891, DateTimeKind.Local).AddTicks(2848), "Default.png", "Admin", 999999999L });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("c2f3eef3-5907-4452-9888-f30aa31feabc"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c2f3eef3-5907-4452-9888-f30aa31feabc"));

            migrationBuilder.DropColumn(
                name: "IsTeacher",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "EmailLink", "ExpireEmailLink", "IsActive", "IsDeleted", "Password", "Phone", "RegisterDate", "UserAvatar", "UserName", "Wallet" },
                values: new object[] { new Guid("00ac29bb-ea32-4fb2-b4b2-58291dc459b7"), "", new Guid("199146e0-efd3-42d6-87af-c27a48b57dc2"), new DateTime(2022, 9, 6, 12, 26, 33, 15, DateTimeKind.Local).AddTicks(4467), true, false, "A6-6A-BB-56-84-C4-59-62-D8-87-56-4F-08-34-6E-8D", 0L, new DateTime(2022, 9, 6, 12, 26, 33, 17, DateTimeKind.Local).AddTicks(6366), "Default.png", "Admin", 999999999L });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("00ac29bb-ea32-4fb2-b4b2-58291dc459b7"));
        }
    }
}
