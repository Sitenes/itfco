using Microsoft.EntityFrameworkCore.Migrations;

namespace Toplearn.Web.Migrations
{
    public partial class AddCourseCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Carts_CartId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CartId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCarts_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCarts_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCarts_CartId",
                table: "CourseCarts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCarts_CourseId",
                table: "CourseCarts",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCarts");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CartId",
                table: "Courses",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Carts_CartId",
                table: "Courses",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
