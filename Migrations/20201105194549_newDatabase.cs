using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMedAPI.Migrations
{
    public partial class newDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostName = table.Column<string>(nullable: false),
                    PostDescription = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryContactApplication",
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
                    ApplicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryContactApplication", x => x.Id);
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
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
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_PrimaryContactApplication_PrimaryContactId",
                        column: x => x.PrimaryContactId,
                        principalTable: "PrimaryContactApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Applications_PrimaryContactId",
                table: "Applications",
                column: "PrimaryContactId");

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
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PrimaryContactApplication");

            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "PrimaryContactResident");
        }
    }
}
