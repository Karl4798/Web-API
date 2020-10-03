using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMedAPI.Migrations
{
    public partial class medication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    GenderString = table.Column<string>(nullable: true),
                    Allergies = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    IdentityNumber = table.Column<string>(nullable: false),
                    MedicalAid = table.Column<string>(nullable: false),
                    MedicalAidNumber = table.Column<string>(nullable: true),
                    DoctorName = table.Column<string>(nullable: true),
                    HomeTelephoneNumber = table.Column<string>(nullable: true),
                    WorkTelephoneNumber = table.Column<string>(nullable: true),
                    CellTelephoneNumber = table.Column<string>(nullable: false),
                    Undertaker = table.Column<string>(nullable: false),
                    UndertakerTelephoneNumber = table.Column<string>(nullable: false),
                    PharmacyName = table.Column<string>(nullable: false),
                    PharmacyTelephoneNumber = table.Column<string>(nullable: false),
                    PharmacyFaxNumber = table.Column<string>(nullable: false),
                    PrimaryContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residents_PrimaryContactResident_PrimaryContactId",
                        column: x => x.PrimaryContactId,
                        principalTable: "PrimaryContactResident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TimeSchedule = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ResidentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_ResidentId",
                table: "Medications",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_PrimaryContactId",
                table: "Residents",
                column: "PrimaryContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Residents");
        }
    }
}
