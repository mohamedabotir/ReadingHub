using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingHub.Persistence.Migrations
{
    public partial class addmybookforBookStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 14, 12, 51, 48, 802, DateTimeKind.Local).AddTicks(4172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 14, 12, 51, 48, 802, DateTimeKind.Local).AddTicks(3862));

            migrationBuilder.CreateTable(
                name: "MyBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BookStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyBooks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyBooks_BookId",
                table: "MyBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_MyBooks_UserId",
                table: "MyBooks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyBooks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 14, 12, 51, 48, 802, DateTimeKind.Local).AddTicks(4172),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 14, 12, 51, 48, 802, DateTimeKind.Local).AddTicks(3862),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(271));
        }
    }
}
