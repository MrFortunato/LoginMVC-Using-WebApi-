using Microsoft.EntityFrameworkCore.Migrations;

namespace Exame_Online.Migrations
{
    public partial class AnoLectivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnoLectivo",
                table: "Candidaturas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Periodo",
                table: "Candidaturas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoLectivo",
                table: "Candidaturas");

            migrationBuilder.DropColumn(
                name: "Periodo",
                table: "Candidaturas");
        }
    }
}
