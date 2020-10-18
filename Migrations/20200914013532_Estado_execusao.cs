using Microsoft.EntityFrameworkCore.Migrations;

namespace Exame_Online.Migrations
{
    public partial class Estado_execusao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_Execusao",
                table: "Testes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado_Execusao",
                table: "Testes");
        }
    }
}
