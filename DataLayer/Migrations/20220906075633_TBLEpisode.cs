using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itfco.Web.Migrations
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
                name: "ProductStatusId",
                table: "ProductStatuses",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProductLevels",
                newName: "LevelTitle");

            migrationBuilder.AddColumn<string>(
                name: "DemoFileName",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubGroupId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Products",
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
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.EpisodeId);
                    table.ForeignKey(
                        name: "FK_Episode_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_GroupId",
                table: "Products",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubGroupId",
                table: "Products",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TeacherId",
                table: "Products",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_ProductId",
                table: "Episode",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Groups_GroupId",
                table: "Products",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Groups_SubGroupId",
                table: "Products",
                column: "SubGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_TeacherId",
                table: "Products",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Groups_GroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Groups_SubGroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_TeacherId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Episode");

            migrationBuilder.DropIndex(
                name: "IX_Products_GroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TeacherId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("00ac29bb-ea32-4fb2-b4b2-58291dc459b7"));

            migrationBuilder.DropColumn(
                name: "DemoFileName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ProductStatuses",
                newName: "ProductStatusId");

            migrationBuilder.RenameColumn(
                name: "LevelTitle",
                table: "ProductLevels",
                newName: "Title");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "EmailLink", "ExpireEmailLink", "IsActive", "IsDeleted", "Password", "Phone", "RegisterDate", "UserAvatar", "UserName", "Wallet" },
                values: new object[] { new Guid("8ffeccab-4e84-4111-8072-f606e4574235"), "", new Guid("de612af7-02b2-4c1f-8942-5c6d0057db93"), new DateTime(2022, 9, 1, 19, 25, 36, 462, DateTimeKind.Local).AddTicks(9145), true, false, "E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E", 0L, new DateTime(2022, 9, 1, 19, 25, 36, 465, DateTimeKind.Local).AddTicks(6236), "Default.png", "Admin", 999999999L });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("8ffeccab-4e84-4111-8072-f606e4574235"));
        }
    }
}
