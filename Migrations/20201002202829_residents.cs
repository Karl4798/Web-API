using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMedAPI.Migrations
{
    public partial class residents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PrimaryContact_PrimaryContactId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TimeSchedule = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: false),
                    ResidentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryContactResident",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Relationship = table.Column<string>(nullable: false),
                    PhysicalAddress = table.Column<string>(nullable: false),
                    PostalAddress = table.Column<string>(nullable: false),
                    PostCode = table.Column<string>(nullable: false),
                    IdentityNumber = table.Column<string>(nullable: false),
                    HomeTelephoneNumber = table.Column<string>(nullable: true),
                    WorkTelephoneNumber = table.Column<string>(nullable: true),
                    CellTelephoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    ResidentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryContactResident", x => x.Id);
                });

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
                    PrimaryContactId = table.Column<int>(nullable: false),
                    MedicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residents_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Residents_PrimaryContactResident_PrimaryContactId",
                        column: x => x.PrimaryContactId,
                        principalTable: "PrimaryContactResident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Residents_MedicationId",
                table: "Residents",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_PrimaryContactId",
                table: "Residents",
                column: "PrimaryContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_PrimaryContactApplication_PrimaryContactId",
                table: "Applications",
                column: "PrimaryContactId",
                principalTable: "PrimaryContactApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PrimaryContactApplication_PrimaryContactId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "PrimaryContactApplication");

            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "PrimaryContactResident");

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    CellTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    CellTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryContact", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_ApplicationId",
                table: "EmergencyContacts",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_PrimaryContact_PrimaryContactId",
                table: "Applications",
                column: "PrimaryContactId",
                principalTable: "PrimaryContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
