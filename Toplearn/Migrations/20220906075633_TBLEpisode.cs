using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class TBLEpisode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8ffeccab-4e84-4111-8072-f606e4574235"));

            migrationBuilder.RenameColumn(
                name: "CourseStatusId",
                table: "CourseStatuses",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "CourseLevels",
                newName: "LevelTitle");

            migrationBuilder.AddColumn<string>(
                name: "DemoFileName",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubGroupId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Episode",
                columns: table => new
                {
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpisodeTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EpisodeFileName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    EpisodeTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.EpisodeId);
                    table.ForeignKey(
                        name: "FK_Episode_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("00ac29bb-ea32-4fb2-b4b2-58291dc459b7"));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_GroupId",
                table: "Courses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubGroupId",
                table: "Courses",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_CourseId",
                table: "Episode",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses",
                column: "GroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses",
                column: "SubGroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Episode");

            migrationBuilder.DropIndex(
                name: "IX_Courses_GroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SubGroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("00ac29bb-ea32-4fb2-b4b2-58291dc459b7"));

            migrationBuilder.DropColumn(
                name: "DemoFileName",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SubGroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "CourseStatuses",
                newName: "CourseStatusId");

            migrationBuilder.RenameColumn(
                name: "LevelTitle",
                table: "CourseLevels",
                newName: "Title");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("8ffeccab-4e84-4111-8072-f606e4574235"));
        }
    }
}
