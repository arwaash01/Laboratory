using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory.Data.Migrations
{
    public partial class initi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalResidenceId = table.Column<int>(type: "int", nullable: false),
                    UniversityNumber = table.Column<int>(type: "int", nullable: false),
                    StudentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    College = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalFileNo = table.Column<int>(type: "int", nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
