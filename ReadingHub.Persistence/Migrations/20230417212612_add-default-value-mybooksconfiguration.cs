using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingHub.Persistence.Migrations
{
    public partial class adddefaultvaluemybooksconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 17, 23, 26, 12, 593, DateTimeKind.Local).AddTicks(9304),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3670));

            migrationBuilder.AlterColumn<int>(
                name: "BookStatusId",
                table: "MyBooks",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 17, 23, 26, 12, 593, DateTimeKind.Local).AddTicks(7926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3329));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 17, 23, 26, 12, 593, DateTimeKind.Local).AddTicks(9304));

            migrationBuilder.AlterColumn<int>(
                name: "BookStatusId",
                table: "MyBooks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 18, 21, 4, 44, 202, DateTimeKind.Local).AddTicks(3329),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 17, 23, 26, 12, 593, DateTimeKind.Local).AddTicks(7926));
        }
    }
}
