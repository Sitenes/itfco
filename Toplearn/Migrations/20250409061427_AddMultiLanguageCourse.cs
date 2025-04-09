using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class AddMultiLanguageCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "TitlePersian");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Courses",
                newName: "ShortDescriptionPersian");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "ShortDescriptionEnglish");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionArabic",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEnglish",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionPersian",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescriptionArabic",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleArabic",
                table: "Courses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleEnglish",
                table: "Courses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionArabic",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DescriptionEnglish",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DescriptionPersian",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ShortDescriptionArabic",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TitleArabic",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TitleEnglish",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "TitlePersian",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ShortDescriptionPersian",
                table: "Courses",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "ShortDescriptionEnglish",
                table: "Courses",
                newName: "Description");
        }
    }
}
