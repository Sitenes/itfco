using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class addNewPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "GroupTitle",
                table: "CourseGroups",
                newName: "NamePersian");

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatorId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "CourseGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEnglish",
                table: "CourseGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentID", "PermissionTitle" },
                values: new object[,]
                {
                    { 18, null, "CategoryManagement" },
                    { 19, null, "AddCategory" },
                    { 20, null, "EditCategory" },
                    { 21, null, "RemoveCategory" }
                });

           
            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 18, 18, 1 },
                    { 19, 19, 1 },
                    { 20, 20, 1 },
                    { 21, 21, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserCreatorId",
                table: "Courses",
                column: "UserCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_UserCreatorId",
                table: "Courses",
                column: "UserCreatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_UserCreatorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserCreatorId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 21);

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "CourseGroups");

            migrationBuilder.DropColumn(
                name: "NameEnglish",
                table: "CourseGroups");

            migrationBuilder.RenameColumn(
                name: "NamePersian",
                table: "CourseGroups",
                newName: "GroupTitle");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
