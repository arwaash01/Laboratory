using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory.Data.Migrations
{
    public partial class indexRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastNameEnglish",
                table: "Requests",
                newName: "FatherNameEnglish");

            migrationBuilder.RenameColumn(
                name: "LastNameArabic",
                table: "Requests",
                newName: "FatherNameArabic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FatherNameEnglish",
                table: "Requests",
                newName: "LastNameEnglish");

            migrationBuilder.RenameColumn(
                name: "FatherNameArabic",
                table: "Requests",
                newName: "LastNameArabic");
        }
    }
}
