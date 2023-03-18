using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingHub.Persistence.Migrations
{
    public partial class addBookStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookStatus",
                table: "MyBooks",
                newName: "BookStatusId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6967),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6509),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(271));

            migrationBuilder.CreateTable(
                name: "BookStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyBooks_BookStatusId",
                table: "MyBooks",
                column: "BookStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyBooks_BookStatus_BookStatusId",
                table: "MyBooks",
                column: "BookStatusId",
                principalTable: "BookStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyBooks_BookStatus_BookStatusId",
                table: "MyBooks");

            migrationBuilder.DropTable(
                name: "BookStatus");

            migrationBuilder.DropIndex(
                name: "IX_MyBooks_BookStatusId",
                table: "MyBooks");

            migrationBuilder.RenameColumn(
                name: "BookStatusId",
                table: "MyBooks",
                newName: "BookStatus");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6967));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 22, 48, 43, 601, DateTimeKind.Local).AddTicks(271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6509));
        }
    }
}
