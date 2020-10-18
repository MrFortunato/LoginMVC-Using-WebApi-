using Exame_Online.Data.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using static Exame_Online.Data.Entidades.IdentyModel;

namespace Exame_Online.Data.Contexto
{
    public static class DataSeed
    {
        public static void SeedData(Contexto ctx, UserManager<Usuario> userManager, RoleManager<Perfil> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            if (!ctx.DadosPessoais.Any())
            {
                DadosPessoais(ctx);
            }
            if (!ctx.TipoPerguntas.Any())
            {
                TipoPerguntas(ctx);
            }
            if (!ctx.Departamentos.Any())
            {
                Departamentos(ctx);
            }
            if (!ctx.Disciplinas.Any())
            {
                Disciplinas(ctx);
            }
        }

        private static void TipoPerguntas(Contexto ctx)
        {
            ctx.TipoPerguntas.AddRange(
                new tb_TipoPergunta { Tipo_Pergunta = "Única" },
                new tb_TipoPergunta { Tipo_Pergunta = "Multiplas" },
                new tb_TipoPergunta { Tipo_Pergunta = "Única Foto" },
                new tb_TipoPergunta { Tipo_Pergunta = "Multiplas Fotos" },
                new tb_TipoPergunta { Tipo_Pergunta = "Texto" }
            );

            ctx.SaveChanges();
        }

        private static void Departamentos(Contexto ctx)
        {
            ctx.Departamentos.AddRange(
                new tb_Departamento { Nome_Departamento = "Engenharia" },
                new tb_Departamento { Nome_Departamento = "Ciências Humanas" },
                new tb_Departamento { Nome_Departamento = "Ciências Sociais" },
                new tb_Departamento { Nome_Departamento = "Gestão" }

            );

            ctx.SaveChanges();
        }

        private static void Disciplinas(Contexto ctx)
        {
            ctx.Disciplinas.AddRange(


                new tb_Disciplina { Nome_Disciplina = "Geografia" },
                new tb_Disciplina { Nome_Disciplina = "Física" },
                new tb_Disciplina { Nome_Disciplina = "Língua Portuguesa" },
                new tb_Disciplina { Nome_Disciplina = "Matemática" }

            );

            ctx.SaveChanges();
        }


        public static void SeedUsers(UserManager<Usuario> userManager)
        {
            if (userManager.FindByNameAsync
            ("Fortunato").Result == null)
            {
                Usuario user = new Usuario();
                user.UserName = "Lisdaura";
                user.Email = "lisdaura@gmail.com";
                IdentityResult result = userManager.CreateAsync
                (user, "123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Administrator").Wait();


                }
            }
          
        }

        private static void DadosPessoais(Contexto ctx)
        {
            ctx.DadosPessoais.Add(
                new tb_DadosPessoais { Nome = "Lisdaura",
                                    SobreNome = "de Almeida", 
                                    Dt_Nasc = Convert.ToDateTime("1995-01-11"), 
                                    Genero = "F",
                                    EstadoCivil = 1,
                                    Nr_BI = "0000000LA023", 
                                    Telefone = "0000000000",
                                    UrlFoto = "user.jpg",
                                    UsuarioId = 1,                             
                   
                }

            );

            ctx.SaveChanges();
        }

        public static void SeedRoles(RoleManager<Perfil> roleManager)
        {
            if (!roleManager.RoleExistsAsync
            ("Candidato").Result)
            {
                Perfil role = new Perfil();
                role.Name = "Candidato";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
            ("Aluno").Result)
            {
                Perfil role = new Perfil();
                role.Name = "Aluno";

                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync
            ("Professor").Result)
            {
                Perfil role = new Perfil();
                role.Name = "Professor";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync
            ("Administrator").Result)
            {
                Perfil role = new Perfil();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            
         
        }    
    }
}
