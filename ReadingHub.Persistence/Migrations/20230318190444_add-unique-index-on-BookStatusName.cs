using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingHub.Persistence.Migrations
{
    public partial class adduniqueindexonBookStatusName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6967));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3329),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6509));

            migrationBuilder.AlterColumn<string>(
                name: "StatusName",
                table: "BookStatus",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookStatus_StatusName",
                table: "BookStatus",
                column: "StatusName",
                unique: true,
                filter: "[StatusName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookStatus_StatusName",
                table: "BookStatus");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6967),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 20, 52, 3, 780, DateTimeKind.Local).AddTicks(6509),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3329));

            migrationBuilder.AlterColumn<string>(
                name: "StatusName",
                table: "BookStatus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
