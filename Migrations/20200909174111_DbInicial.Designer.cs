﻿// <auto-generated />
using System;
using Exame_Online.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exame_Online.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200909174111_DbInicial")]
    partial class DbInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Perfil");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+PerfilClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("PerfilClaim");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+PerfilUsuario", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PerfilUsuario");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UsuarioId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnName("Nome_Usuario")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+UsuarioClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UsuarioClaim");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+UsuarioLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UsuarioLogin");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+UsuarioToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UsuarioToken");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Agendamento_Teste", b =>
                {
                    b.Property<int>("AgendamentoTesteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data_Exp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_Realizacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplinaId")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("Sala_Exame_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Sala_Exame_Id1")
                        .HasColumnType("int");

                    b.HasKey("AgendamentoTesteId");

                    b.HasIndex("CursoId");

                    b.HasIndex("DisciplinaId");

                    b.HasIndex("Sala_Exame_Id1");

                    b.ToTable("AgendamentosTestes");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Candidatura", b =>
                {
                    b.Property<int>("CandidaturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DadosPessoaisId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dt_Registro")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.HasKey("CandidaturaId");

                    b.HasIndex("DadosPessoaisId");

                    b.ToTable("Candidaturas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome_Curso")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("CursoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_DadosPessoais", b =>
                {
                    b.Property<int>("DadosPessoaisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Dt_Nasc")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EstadoCivil")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nr_BI")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("SobreNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("UrlFoto")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("DadosPessoaisId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("DadosPessoais");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome_Departamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("DepartamentoId");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Disciplina", b =>
                {
                    b.Property<int>("DisciplinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome_Disciplina")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("DisciplinaId");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Opcoes_Candidatura", b =>
                {
                    b.Property<int>("OpcaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CandidaturaId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Opcao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OpcaoId");

                    b.HasIndex("CandidaturaId");

                    b.HasIndex("CursoId");

                    b.ToTable("opcoesCandidaturas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Pergunta", b =>
                {
                    b.Property<int>("PerguntaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisciplinaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoPerguntaId")
                        .HasColumnType("int");

                    b.Property<string>("UrlImagem")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("PerguntaId");

                    b.HasIndex("DisciplinaId");

                    b.HasIndex("TipoPerguntaId");

                    b.ToTable("Perguntas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Resposta", b =>
                {
                    b.Property<int>("RespostaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("PerguntaId")
                        .HasColumnType("int");

                    b.Property<string>("Resposta_Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(4, 2)");

                    b.HasKey("RespostaId");

                    b.HasIndex("PerguntaId");

                    b.ToTable("Respostas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Sala_Exame", b =>
                {
                    b.Property<int>("Sala_Exame_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("Lotacao")
                        .HasColumnType("int");

                    b.HasKey("Sala_Exame_Id");

                    b.ToTable("Sala_Exames");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Teste", b =>
                {
                    b.Property<int>("TesteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgendamentoTesteId")
                        .HasColumnType("int");

                    b.Property<int>("CandidaturaId")
                        .HasColumnType("int");

                    b.Property<int>("DisciplinaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dt_Realizacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duracacao")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.HasKey("TesteId");

                    b.HasIndex("AgendamentoTesteId");

                    b.HasIndex("CandidaturaId");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("Testes");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_TesteDisciplina", b =>
                {
                    b.Property<int>("DisciplinaId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("Perguntas")
                        .HasColumnType("int");

                    b.HasKey("DisciplinaId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("TesteDisciplinas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Teste_Pergunta", b =>
                {
                    b.Property<int>("TestePerguntaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("Nr_Pergunta")
                        .HasColumnType("int");

                    b.Property<int>("PerguntaId")
                        .HasColumnType("int");

                    b.Property<int>("TesteId")
                        .HasColumnType("int");

                    b.HasKey("TestePerguntaId");

                    b.HasIndex("PerguntaId");

                    b.HasIndex("TesteId");

                    b.ToTable("TestePerguntas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Teste_Resultado", b =>
                {
                    b.Property<int>("TesteResultadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cotacao")
                        .HasColumnType("decimal(4, 2)");

                    b.Property<int>("RespostaId")
                        .HasColumnType("int");

                    b.Property<string>("RespostaTexte")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("TesteId")
                        .HasColumnType("int");

                    b.Property<int>("TestePerguntaId")
                        .HasColumnType("int");

                    b.HasKey("TesteResultadoId");

                    b.HasIndex("RespostaId");

                    b.HasIndex("TesteId");

                    b.HasIndex("TestePerguntaId");

                    b.ToTable("TesteResultados");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_TipoPergunta", b =>
                {
                    b.Property<int>("TipoPerguntaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo_Pergunta")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("TipoPerguntaId");

                    b.ToTable("TipoPerguntas");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+PerfilClaim", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.IdentyModel+Perfil", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+PerfilUsuario", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.IdentyModel+Perfil", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.IdentyModel+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+UsuarioClaim", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.IdentyModel+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+UsuarioLogin", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.IdentyModel+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.IdentyModel+UsuarioToken", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.IdentyModel+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Agendamento_Teste", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Curso", "Curso")
                        .WithMany("AgendamentoTestes")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Disciplina", "Disciplina")
                        .WithMany("AgendamentosTestes")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Sala_Exame", "Sala_Exame")
                        .WithMany("AgendamentoTestes")
                        .HasForeignKey("Sala_Exame_Id1");
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Candidatura", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_DadosPessoais", "DadosPessoais")
                        .WithMany("Candidaturas")
                        .HasForeignKey("DadosPessoaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Curso", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Departamento", "Departamento")
                        .WithMany("Cursos")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_DadosPessoais", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.IdentyModel+Usuario", "Usuario")
                        .WithOne("Pessoa")
                        .HasForeignKey("Exame_Online.Data.Entidades.tb_DadosPessoais", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Opcoes_Candidatura", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Candidatura", "Candidatura")
                        .WithMany("OpcoesCandidaturas")
                        .HasForeignKey("CandidaturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Curso", "Curso")
                        .WithMany("OpcoesCandidaturas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Pergunta", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Disciplina", "Disciplina")
                        .WithMany()
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_TipoPergunta", "TipoPergunta")
                        .WithMany("Perguntas")
                        .HasForeignKey("TipoPerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Resposta", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Pergunta", "Pergunta")
                        .WithMany("Respostas")
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Teste", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Agendamento_Teste", "AgendamentoTeste")
                        .WithMany("Testes")
                        .HasForeignKey("AgendamentoTesteId");

                    b.HasOne("Exame_Online.Data.Entidades.tb_Candidatura", "Candidatura")
                        .WithMany("Teste")
                        .HasForeignKey("CandidaturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Disciplina", "Disciplina")
                        .WithMany("Testes")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_TesteDisciplina", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Disciplina", "Disciplina")
                        .WithMany()
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Teste_Pergunta", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Pergunta", "Pergunta")
                        .WithMany("TestePerguntas")
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Teste", "Teste")
                        .WithMany()
                        .HasForeignKey("TesteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exame_Online.Data.Entidades.tb_Teste_Resultado", b =>
                {
                    b.HasOne("Exame_Online.Data.Entidades.tb_Resposta", "Resposta")
                        .WithMany("TesteResultados")
                        .HasForeignKey("RespostaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Teste", "Teste")
                        .WithMany("TesteResultados")
                        .HasForeignKey("TesteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exame_Online.Data.Entidades.tb_Teste_Pergunta", "TestePergunta")
                        .WithMany()
                        .HasForeignKey("TestePerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
