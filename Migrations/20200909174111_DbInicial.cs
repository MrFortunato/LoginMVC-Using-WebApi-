using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exame_Online.Migrations
{
    public partial class DbInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Departamento = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.DepartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    DisciplinaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Disciplina = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.DisciplinaId);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sala_Exames",
                columns: table => new
                {
                    Sala_Exame_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(maxLength: 10, nullable: false),
                    Lotacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala_Exames", x => x.Sala_Exame_Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPerguntas",
                columns: table => new
                {
                    TipoPerguntaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Pergunta = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPerguntas", x => x.TipoPerguntaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Usuario = table.Column<string>(maxLength: 20, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Curso = table.Column<string>(maxLength: 100, nullable: false),
                    DepartamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Cursos_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilClaim_Perfil_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perguntas",
                columns: table => new
                {
                    PerguntaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    TipoPerguntaId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    UrlImagem = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perguntas", x => x.PerguntaId);
                    table.ForeignKey(
                        name: "FK_Perguntas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perguntas_TipoPerguntas_TipoPerguntaId",
                        column: x => x.TipoPerguntaId,
                        principalTable: "TipoPerguntas",
                        principalColumn: "TipoPerguntaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadosPessoais",
                columns: table => new
                {
                    DadosPessoaisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    SobreNome = table.Column<string>(maxLength: 50, nullable: false),
                    Dt_Nasc = table.Column<DateTime>(nullable: false),
                    Genero = table.Column<string>(maxLength: 10, nullable: false),
                    EstadoCivil = table.Column<int>(nullable: false),
                    Nr_BI = table.Column<string>(maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false),
                    UrlFoto = table.Column<string>(maxLength: 255, nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosPessoais", x => x.DadosPessoaisId);
                    table.ForeignKey(
                        name: "FK_DadosPessoais_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilUsuario",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilUsuario", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PerfilUsuario_Perfil_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilUsuario_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioClaim_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UsuarioLogin_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioToken",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UsuarioToken_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendamentosTestes",
                columns: table => new
                {
                    AgendamentoTesteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_Realizacao = table.Column<DateTime>(nullable: false),
                    Data_Exp = table.Column<DateTime>(nullable: false),
                    Horario = table.Column<string>(maxLength: 10, nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    Sala_Exame_Id = table.Column<int>(nullable: false),
                    Sala_Exame_Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentosTestes", x => x.AgendamentoTesteId);
                    table.ForeignKey(
                        name: "FK_AgendamentosTestes_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendamentosTestes_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendamentosTestes_Sala_Exames_Sala_Exame_Id1",
                        column: x => x.Sala_Exame_Id1,
                        principalTable: "Sala_Exames",
                        principalColumn: "Sala_Exame_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TesteDisciplinas",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    Duracao = table.Column<int>(nullable: false),
                    Perguntas = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TesteDisciplinas", x => new { x.DisciplinaId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_TesteDisciplinas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TesteDisciplinas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respostas",
                columns: table => new
                {
                    RespostaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resposta_Desc = table.Column<string>(nullable: false),
                    PerguntaId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(4, 2)", nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respostas", x => x.RespostaId);
                    table.ForeignKey(
                        name: "FK_Respostas_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "PerguntaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    CandidaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dt_Registro = table.Column<DateTime>(nullable: false),
                    DadosPessoaisId = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.CandidaturaId);
                    table.ForeignKey(
                        name: "FK_Candidaturas_DadosPessoais_DadosPessoaisId",
                        column: x => x.DadosPessoaisId,
                        principalTable: "DadosPessoais",
                        principalColumn: "DadosPessoaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "opcoesCandidaturas",
                columns: table => new
                {
                    OpcaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(nullable: false),
                    Opcao = table.Column<string>(nullable: true),
                    CandidaturaId = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opcoesCandidaturas", x => x.OpcaoId);
                    table.ForeignKey(
                        name: "FK_opcoesCandidaturas_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "CandidaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_opcoesCandidaturas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Testes",
                columns: table => new
                {
                    TesteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidaturaId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    Dt_Realizacao = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    Duracacao = table.Column<int>(nullable: false),
                    AgendamentoTesteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testes", x => x.TesteId);
                    table.ForeignKey(
                        name: "FK_Testes_AgendamentosTestes_AgendamentoTesteId",
                        column: x => x.AgendamentoTesteId,
                        principalTable: "AgendamentosTestes",
                        principalColumn: "AgendamentoTesteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Testes_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "CandidaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Testes_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestePerguntas",
                columns: table => new
                {
                    TestePerguntaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TesteId = table.Column<int>(nullable: false),
                    PerguntaId = table.Column<int>(nullable: false),
                    Nr_Pergunta = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestePerguntas", x => x.TestePerguntaId);
                    table.ForeignKey(
                        name: "FK_TestePerguntas_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "PerguntaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestePerguntas_Testes_TesteId",
                        column: x => x.TesteId,
                        principalTable: "Testes",
                        principalColumn: "TesteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TesteResultados",
                columns: table => new
                {
                    TesteResultadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TesteId = table.Column<int>(nullable: false),
                    TestePerguntaId = table.Column<int>(nullable: false),
                    RespostaId = table.Column<int>(nullable: false),
                    RespostaTexte = table.Column<string>(maxLength: 50, nullable: true),
                    Cotacao = table.Column<decimal>(type: "decimal(4, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TesteResultados", x => x.TesteResultadoId);
                    table.ForeignKey(
                        name: "FK_TesteResultados_Respostas_RespostaId",
                        column: x => x.RespostaId,
                        principalTable: "Respostas",
                        principalColumn: "RespostaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TesteResultados_Testes_TesteId",
                        column: x => x.TesteId,
                        principalTable: "Testes",
                        principalColumn: "TesteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TesteResultados_TestePerguntas_TestePerguntaId",
                        column: x => x.TestePerguntaId,
                        principalTable: "TestePerguntas",
                        principalColumn: "TestePerguntaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosTestes_CursoId",
                table: "AgendamentosTestes",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosTestes_DisciplinaId",
                table: "AgendamentosTestes",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosTestes_Sala_Exame_Id1",
                table: "AgendamentosTestes",
                column: "Sala_Exame_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_DadosPessoaisId",
                table: "Candidaturas",
                column: "DadosPessoaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_DepartamentoId",
                table: "Cursos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosPessoais_UsuarioId",
                table: "DadosPessoais",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_opcoesCandidaturas_CandidaturaId",
                table: "opcoesCandidaturas",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_opcoesCandidaturas_CursoId",
                table: "opcoesCandidaturas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Perfil",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilClaim_RoleId",
                table: "PerfilClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilUsuario_RoleId",
                table: "PerfilUsuario",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Perguntas_DisciplinaId",
                table: "Perguntas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Perguntas_TipoPerguntaId",
                table: "Perguntas",
                column: "TipoPerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_PerguntaId",
                table: "Respostas",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_TesteDisciplinas_CursoId",
                table: "TesteDisciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_TestePerguntas_PerguntaId",
                table: "TestePerguntas",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_TestePerguntas_TesteId",
                table: "TestePerguntas",
                column: "TesteId");

            migrationBuilder.CreateIndex(
                name: "IX_TesteResultados_RespostaId",
                table: "TesteResultados",
                column: "RespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_TesteResultados_TesteId",
                table: "TesteResultados",
                column: "TesteId");

            migrationBuilder.CreateIndex(
                name: "IX_TesteResultados_TestePerguntaId",
                table: "TesteResultados",
                column: "TestePerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_AgendamentoTesteId",
                table: "Testes",
                column: "AgendamentoTesteId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_CandidaturaId",
                table: "Testes",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_DisciplinaId",
                table: "Testes",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioClaim_UserId",
                table: "UsuarioClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogin_UserId",
                table: "UsuarioLogin",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "opcoesCandidaturas");

            migrationBuilder.DropTable(
                name: "PerfilClaim");

            migrationBuilder.DropTable(
                name: "PerfilUsuario");

            migrationBuilder.DropTable(
                name: "TesteDisciplinas");

            migrationBuilder.DropTable(
                name: "TesteResultados");

            migrationBuilder.DropTable(
                name: "UsuarioClaim");

            migrationBuilder.DropTable(
                name: "UsuarioLogin");

            migrationBuilder.DropTable(
                name: "UsuarioToken");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "Respostas");

            migrationBuilder.DropTable(
                name: "TestePerguntas");

            migrationBuilder.DropTable(
                name: "Perguntas");

            migrationBuilder.DropTable(
                name: "Testes");

            migrationBuilder.DropTable(
                name: "TipoPerguntas");

            migrationBuilder.DropTable(
                name: "AgendamentosTestes");

            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Sala_Exames");

            migrationBuilder.DropTable(
                name: "DadosPessoais");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
