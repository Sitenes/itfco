using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class fixNullableCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLevels_LevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CourseStatusId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses",
                column: "GroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLevels_LevelId",
                table: "Courses",
                column: "LevelId",
                principalTable: "CourseLevels",
                principalColumn: "CourseLevelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusId",
                table: "Courses",
                column: "CourseStatusId",
                principalTable: "CourseStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLevels_LevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseStatusId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses",
                column: "GroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLevels_LevelId",
                table: "Courses",
                column: "LevelId",
                principalTable: "CourseLevels",
                principalColumn: "CourseLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusId",
                table: "Courses",
                column: "CourseStatusId",
                principalTable: "CourseStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
