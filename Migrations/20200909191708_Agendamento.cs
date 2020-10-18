using Microsoft.EntityFrameworkCore.Migrations;

namespace Exame_Online.Migrations
{
    public partial class Agendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentosTestes_Sala_Exames_Sala_Exame_Id1",
                table: "AgendamentosTestes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sala_Exames",
                table: "Sala_Exames");

            migrationBuilder.DropIndex(
                name: "IX_AgendamentosTestes_Sala_Exame_Id1",
                table: "AgendamentosTestes");

            migrationBuilder.DropColumn(
                name: "Sala_Exame_Id",
                table: "Sala_Exames");

            migrationBuilder.DropColumn(
                name: "Sala_Exame_Id",
                table: "AgendamentosTestes");

            migrationBuilder.DropColumn(
                name: "Sala_Exame_Id1",
                table: "AgendamentosTestes");

            migrationBuilder.AddColumn<int>(
                name: "SalaExameId",
                table: "Sala_Exames",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SalaExameId",
                table: "AgendamentosTestes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sala_Exames",
                table: "Sala_Exames",
                column: "SalaExameId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosTestes_SalaExameId",
                table: "AgendamentosTestes",
                column: "SalaExameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentosTestes_Sala_Exames_SalaExameId",
                table: "AgendamentosTestes",
                column: "SalaExameId",
                principalTable: "Sala_Exames",
                principalColumn: "SalaExameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentosTestes_Sala_Exames_SalaExameId",
                table: "AgendamentosTestes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sala_Exames",
                table: "Sala_Exames");

            migrationBuilder.DropIndex(
                name: "IX_AgendamentosTestes_SalaExameId",
                table: "AgendamentosTestes");

            migrationBuilder.DropColumn(
                name: "SalaExameId",
                table: "Sala_Exames");

            migrationBuilder.DropColumn(
                name: "SalaExameId",
                table: "AgendamentosTestes");

            migrationBuilder.AddColumn<int>(
                name: "Sala_Exame_Id",
                table: "Sala_Exames",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Sala_Exame_Id",
                table: "AgendamentosTestes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sala_Exame_Id1",
                table: "AgendamentosTestes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sala_Exames",
                table: "Sala_Exames",
                column: "Sala_Exame_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosTestes_Sala_Exame_Id1",
                table: "AgendamentosTestes",
                column: "Sala_Exame_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentosTestes_Sala_Exames_Sala_Exame_Id1",
                table: "AgendamentosTestes",
                column: "Sala_Exame_Id1",
                principalTable: "Sala_Exames",
                principalColumn: "Sala_Exame_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
