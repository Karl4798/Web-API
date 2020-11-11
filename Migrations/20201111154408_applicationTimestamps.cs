using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMedAPI.Migrations
{
    public partial class applicationTimestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomNumber",
                table: "Residents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Applications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Applications");
        }
    }
}