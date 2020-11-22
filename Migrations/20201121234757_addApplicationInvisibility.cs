using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMedAPI.Migrations
{
    public partial class addApplicationInvisibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Applications");

            migrationBuilder.AddColumn<bool>(
                name: "Invisible",
                table: "Applications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invisible",
                table: "Applications");

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
