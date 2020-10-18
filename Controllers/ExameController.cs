using Exame_Online.Data.Contexto;
using Exame_Online.Data.Entidades;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static Exame_Online.ModelsView.RespostaModel;

namespace Exame_Online.Controllers
{
    [Authorize(Roles ="Candidato,Administrador")]
    public class ExameController : Controller
    {
        private readonly Contexto db;
        public ExameController(Contexto _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult Online(int? nrP, int? Id)
        {

            if (nrP.GetValueOrDefault() < 1)
                nrP = 1;


            var testPergunta = db.TestePerguntas.Include(x => x.Pergunta)
                .ThenInclude(x => x.TipoPergunta)
                .Where(x => x.TesteId == Id && x.Nr_Pergunta == nrP)
                .Select(x => x.TestePerguntaId)
                .FirstOrDefault();

            if (testPergunta > 0)
            {
                var model = db.TestePerguntas
                    .Where(x => x.TestePerguntaId == testPergunta)
                    .Select(x => new RespostaModel()
                    {
                        TotalPerguntas = db.TestePerguntas.Where(x => x.TestePerguntaId == testPergunta).Count(),
                        TipoPergunta = x.Pergunta.TipoPergunta.Tipo_Pergunta,
                        Pergunta = x.Pergunta.Descricao,
                        Disciplina = x.Pergunta.Disciplina.Nome_Disciplina,
                        Pergunta_Nr = x.Nr_Pergunta,
                        Valor = x.Pergunta.Valor,
                        Id_Teste = x.TesteId,
                        Opcoes = x.Pergunta.Respostas.Where(e => e.Estado == true).Select(e => new PertuntaXModel()
                        {
                            Id_Opcao = e.RespostaId,
                            Label = e.Resposta_Desc
                        }).ToList()

                    }).FirstOrDefault();

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }


        [HttpGet]
        public JsonResult Add(int IdTeste, int Nr_P, string[] IdOpcao, int Id, string resposta, int direcao)
        {
            var Resultado = "";

            if (IdTeste == 0 || Nr_P == 0)
            {
                return Json("4");
            }
            // Selecionar pergunta 
            var testePergunta = db.TestePerguntas
               .Include(x => x.Pergunta)
               .ThenInclude(x => x.TipoPergunta)
               .Where(x => x.TesteId == IdTeste
                && x.Nr_Pergunta == Nr_P).
               Select(x => new
               {
                   T_Id = x.TesteId, // Teste Id
                   TP_Id = x.TestePerguntaId, //TestePergunta Id
                   PT = x.Pergunta.TipoPergunta.Tipo_Pergunta, // Tipo Pergunta
                   QId = x.PerguntaId,   // Pergunta Id
                   Vlr = x.Pergunta.Valor // Cotação Pergunta
               }).FirstOrDefault();

            if (IdOpcao != null && Id == 0 || resposta == null)
            {

                if (testePergunta != null)
                {
                    //Pergunta com um unica opcão de Resposta /RESPOSTA ÚNICA
                    var respostax = db.TesteResultados
                                .Where(x => x.TesteId == Convert.ToInt32(testePergunta.T_Id)
                                && x.TestePerguntaId == Convert.ToInt32(testePergunta.TP_Id)
                                ).FirstOrDefault();

                    if (IdOpcao.Length > 0)
                    {
                        if(testePergunta.PT.ToString()== "Única")
                        {
                            //Verificar se a pergunta já está respondida 
                           
                            for (int i = 0; i < IdOpcao.Count(); i++)
                            {
                                if (respostax != null)
                                {
                                    // Actualizar resposta já cadastrada /RESPOSTA ÚNICA
                                    var res = db.TesteResultados.FirstOrDefault(x => x.TesteResultadoId == respostax.TesteResultadoId);
                                    if (res != null)
                                    {
                                        res.RespostaId = Convert.ToInt32(IdOpcao[i]);
                                        res.RespostaTexte = "SELECIONADA";
                                        res.Cotacao = Convert.ToDecimal(db.Respostas.FirstOrDefault(x => x.RespostaId == Convert.ToInt32(IdOpcao[i])).Valor);
                                        db.TesteResultados.Update(res);
                                        db.SaveChanges();

                                        if(direcao == 0)
                                        {
                                            Resultado = Convert.ToString(Nr_P - 1);
                                        }
                                        else
                                        {
                                            Resultado = Convert.ToString(Nr_P + 1);
                                        }
                                       
                                        return Json(Resultado);
                                    }
                                }
                            }
                            // Responder pela 1ª vez /RESPOSTA ÚNICA
                            foreach (var item in IdOpcao)
                            {

                                var tst = new tb_Teste_Resultado
                                {
                                    TesteId = testePergunta.T_Id,
                                    TestePerguntaId = Convert.ToInt32(testePergunta.TP_Id),
                                    RespostaId = Convert.ToInt32(item),
                                    RespostaTexte = "SELECIONADA",
                                    Cotacao = Convert.ToDecimal(db.Respostas.FirstOrDefault(x => x.RespostaId == Convert.ToInt32(item)).Valor)
                                };
                                db.TesteResultados.Add(tst);
                                db.SaveChanges();
                                if (direcao == 0)
                                {
                                    Resultado = Convert.ToString(Nr_P - 1);
                                }
                                else
                                {
                                    Resultado = Convert.ToString(Nr_P + 1);
                                }
                                return Json(Resultado);
                            };

                        }
                        else
                        {
                            for (int i = 0; i < IdOpcao.Count(); i++)
                            {
                                if (respostax != null)
                                {
                                    // Actualizar resposta já respondida /RESPOSTA MULTIPLAS
                                    var res = db.TesteResultados.Where(x => x.TesteId == IdTeste && x.TestePerguntaId == testePergunta.TP_Id).ToList();
                                    if (res != null)
                                    {
                                       
                                        foreach(var item in res)
                                        {
                                            var resp = db.TesteResultados
                                                .Where(x => x.TesteResultadoId == item.TesteResultadoId
                                                 ).FirstOrDefault();
                                            //Desmarcando todas as reposta já respondida na Base de dados SEM EXCESSÃO
                                            if (resp!= null)
                                            {
                                                resp.RespostaTexte= null;
                                                resp.Cotacao = Convert.ToDecimal(0);
                                                db.TesteResultados.Update(resp);
                                                db.SaveChanges();
                                            }
                                        }
                                        // Depois de selecionamos todas as repostas inactivas com base no Opção pretendida (Id)
                                        foreach (var item in IdOpcao)
                                        {
                                            var r = db.TesteResultados
                                                   .Where(x => x.TesteId == IdTeste
                                                   && x.TestePerguntaId == testePergunta.TP_Id
                                                   && x.RespostaId == Convert.ToInt32(item))
                                                   .FirstOrDefault();

                                            // Se estives Inactiva activR
                                            if (r != null)
                                            {
                                                r.RespostaTexte= "SELECIONADA";
                                                r.Cotacao = Convert.ToDecimal(db.Respostas.FirstOrDefault(x => x.RespostaId == Convert.ToInt32(item)).Valor);
                                                db.TesteResultados.Update(r);
                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                var tst = new tb_Teste_Resultado
                                                {
                                                    TesteId = testePergunta.T_Id,
                                                    TestePerguntaId = Convert.ToInt32(testePergunta.TP_Id),
                                                    RespostaId = Convert.ToInt32(item),
                                                    RespostaTexte= "SELECIONADA",
                                                    Cotacao = Convert.ToDecimal(db.Respostas.FirstOrDefault(x => x.RespostaId == Convert.ToInt32(item)).Valor)
                                                };
                                                db.TesteResultados.Add(tst);
                                                db.SaveChanges();
                                            }

                                        };
                                        if (direcao == 0)
                                        {
                                            Resultado = Convert.ToString(Nr_P - 1);
                                        }
                                        else
                                        {
                                            Resultado = Convert.ToString(Nr_P + 1);
                                        }
                                        return Json(Resultado);

                                    }
                  

                                }

                            }
                           //Responder pela primeira vez/ Multiplas 
                            foreach (var item in IdOpcao)
                            {

                                var tst = new tb_Teste_Resultado
                                {
                                    TesteId = testePergunta.T_Id,
                                    TestePerguntaId = Convert.ToInt32(testePergunta.TP_Id),
                                    RespostaId = Convert.ToInt32(item),
                                    RespostaTexte = "SELECIONADA",
                                    Cotacao = Convert.ToDecimal(db.Respostas.FirstOrDefault(x => x.RespostaId == Convert.ToInt32(item)).Valor)
                                };
                                db.TesteResultados.Add(tst);
                                db.SaveChanges();
                               
                            };
                            if (direcao == 0)
                            {
                                Resultado = Convert.ToString(Nr_P - 1);
                            }
                            else
                            {
                                Resultado = Convert.ToString(Nr_P + 1);
                            }
                            return Json(Resultado);
                        }

                    }
                    // Se chegar... aqui não foi selecionado ou digitado alguma resposta

                    if (direcao == 0)
                    {
                        Resultado = Convert.ToString(Nr_P - 1);
                    }
                    else
                    {
                        Resultado = Convert.ToString(Nr_P + 1);
                    }
                  
                    return Json(Resultado);
                }

            }
            // RespostaTexteDigitada pelo Candidado
            if (Id > 0 && resposta != null)
            {
                var respostax = db.TesteResultados
                                 .Where(x => x.TesteId == Convert.ToInt32(testePergunta.T_Id)
                                 && x.TestePerguntaId == Convert.ToInt32(testePergunta.TP_Id)
                                 && x.RespostaId == Id).FirstOrDefault();
                if (respostax != null)
                {
                    respostax.RespostaTexte= null;
                    db.TesteResultados.Remove(respostax);
                    db.SaveChanges();
                }
                var tst = new tb_Teste_Resultado
                {
                    TesteId = testePergunta.T_Id,
                    TestePerguntaId = Convert.ToInt32(testePergunta.TP_Id),
                    RespostaId = Id,
                    RespostaTexte= resposta,
                    Cotacao = Convert.ToDecimal(db.Respostas.FirstOrDefault(x => x.RespostaId == Id).Valor)
                };
                db.TesteResultados.Add(tst);
                db.SaveChanges();
                if (direcao == 0)
                {
                    Resultado = Convert.ToString(Nr_P - 1);
                }
                else
                {
                    Resultado = Convert.ToString(Nr_P + 1);
                }
                return Json(Resultado);

            }
            // Se chegar aqui... IdTeste ou NrPergunta estão zeradas
          
                Resultado = "Erro";
       
            return Json(Resultado);

        }


        [HttpGet]
        public List<PerguntaModelView> TotalPerguntas(int? Id)
        {
            if (Id == null)
            {

            }
            var perguntas = db.TestePerguntas.Where(x => x.TesteId == Id && x.Estado == true)
                .Include(x => x.Pergunta)
                .ThenInclude(x => x.TipoPergunta)
                .Include(x => x.Teste)
                .ThenInclude(x => x.Candidatura)
                .Include(x => x.Teste.Disciplina)
                .ToList().OrderBy(x=>x.Nr_Pergunta);

            var lista = new List<PerguntaModelView>();

            foreach (var item in perguntas)
            {
                lista.Add(new PerguntaModelView
                {
                    IdTeste = item.TesteId,
                    Id_Pergunta = item.TestePerguntaId,
                    Pergunta = item.Pergunta.Descricao,
                    Disciplina = item.Pergunta.TipoPergunta.Tipo_Pergunta,
                    NrPergunta = item.Nr_Pergunta,
                    Valor = item.Pergunta.Valor,


                });
            }
            return lista;
        }

        [HttpGet]
        public PerguntaModelView SelecionarPertunta(int? nrP, int? Id)
        {
            if (nrP == null || Id == null)
            {
                return null;
            }

          
            var total = db.TestePerguntas.Where(x => x.TesteId == Id && x.Estado == true).Count();

            if( nrP > total)
            {
                nrP = 1;
            }
            else if( nrP < 1)
            {
                nrP = total;
            }

            var p = db.TestePerguntas
                .Include(x => x.Pergunta)
                .ThenInclude(x => x.TipoPergunta)
                .Where(x => x.Nr_Pergunta == nrP &&
                x.TesteId == Id &&
                x.Estado == true).FirstOrDefault();
            if (p == null)
            {
              
                return null;
            }
            var resultado = new PerguntaModelView
            {
                Id_Pergunta = p.Nr_Pergunta,
                Pergunta = p.Pergunta.Descricao,
                TipoPergunta = p.Pergunta.TipoPergunta.Tipo_Pergunta,
                Total = db.TestePerguntas.Where(x => x.TesteId == Id && x.Estado == true).Count(),
               

        };
            var tp = db.TestePerguntas.Where(x => x.TesteId == Id &&
                x.Estado == true).ToList();
            //------- Contagem das perguntas já respondidas
            foreach (var item in tp)
            {
              
                resultado.TotalRespondidas += db.TesteResultados
                                            .Where(x => x.TestePerguntaId == item.TestePerguntaId 
                                            && x.TesteId == item.TesteId 
                                            && x.RespostaTexte!= null)
                                            .Select(x => x.TestePerguntaId)
                                            .Distinct().Count();
            }

            return resultado;
        }

        [HttpGet]
        public IActionResult Respostas(int? nrP, int? Id)
        {
            if (Id == null)
            {
                return null;
            }
            if (nrP == null || Id == null)
            {
                return null;
            }

            var total = db.TestePerguntas.Where(x => x.TesteId == Id && x.Estado == true).Count();

            if (nrP > total)
            {
                nrP = 1;
            }
            else if (nrP < 1)
            {
                nrP = total;
            }

            var testPergunta = db.TestePerguntas
                .Include(x => x.Pergunta)
                .ThenInclude(x => x.TipoPergunta)
                .Where(x => x.Nr_Pergunta == nrP &&
                x.TesteId == Id &&
                x.Estado == true).FirstOrDefault();
          
            if (testPergunta.PerguntaId > 0)
            {


            }
            var model = db.Respostas.Include(x => x.Pergunta)
                .ThenInclude(x => x.TipoPergunta)
                        .Where(e => e.Estado == true && e.PerguntaId == testPergunta.PerguntaId)
                        .Select(e => new EscolhaModels()
                        {
                            TipoPergunta = e.Pergunta.TipoPergunta.Tipo_Pergunta,
                            Id_Escolha = e.RespostaId,
                            respostaView = e.Resposta_Desc
                        }).ToList();

            // Verificar se a RespostaTexte já está registrada na base de dados
            var respostasx = db.TesteResultados
                .Include(x => x.Resposta).Where(x => x.TesteId == Id
                 && x.TestePerguntaId == testPergunta.TestePerguntaId
                 && x.RespostaTexte!= null
                 && x.Resposta.Estado == true)
                .Select(x => new
                {
                    Id = x.RespostaId,
                    Res = x.RespostaTexte,
                    Respostass = x.Resposta.Resposta_Desc,

                })
                .ToList();

            foreach (var item in respostasx)
            {
                model.Where(x => x.Id_Escolha == item.Id).ToList().FirstOrDefault().RespostaTexto = item.Res;
                model.Where(x => x.Id_Escolha == item.Id).ToList().FirstOrDefault().respostaView = item.Respostass;
                model.Where(x => x.Id_Escolha == item.Id).ToList().FirstOrDefault().Selecionado = Convert.ToString(item.Id);

            }

            return Json(model);
        }
        [HttpGet]
        public IActionResult SelecionatTeste(int IdDisciplina,int IdCurso, string Data, int IdSala, string Hora)
        {

            var Agendamento = db.AgendamentosTestes
                .FirstOrDefault(x => x.Data_Realizacao == Convert.ToDateTime(Data)
                && x.CursoId == IdCurso
                && x.Horario == Hora
                && x.DisciplinaId == IdDisciplina && x.SalaExameId== IdSala);

            if(Agendamento ==null)
            {
                return Json("2");
            }

            var Testes = db.Testes
                .Include(x => x.Disciplina)
                .Include(x => x.Candidatura)
                .ThenInclude(x => x.DadosPessoais)
                .Where(x => x.DisciplinaId == Agendamento.DisciplinaId
                && x.AgendamentoTesteId == Agendamento.AgendamentoTesteId && x.Estado == true)
                .Select(x => new CandidatoViewModel()
                {
                    Id_Teste = x.TesteId,
                    Disciplina = x.Disciplina.Nome_Disciplina,
                    Nome_Completo = x.Candidatura.DadosPessoais.Nome + " " + x.Candidatura.DadosPessoais.SobreNome,
                    Dt_Agendamento = x.AgendamentoTeste.Data_Realizacao.ToShortDateString(),
                    Curso = x.AgendamentoTeste.Curso.Nome_Curso,
                    Selecionado = x.Estado_Execusao.ToString()

                }).ToList();

            if(Testes == null)
            {
                return Json("3");
            }

            return Json(Testes);
        }


        [HttpGet]
        public PerguntaModelView Responde(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var p = db.Perguntas.FirstOrDefault(x => x.PerguntaId == id);

            if (p == null)
            {
                return null;

            };
            var pergunta = new PerguntaModelView
            {
                Id_Pergunta = p.PerguntaId,
                Pergunta = p.Descricao,
            };

            return pergunta;
        }


        [HttpGet]
        public IActionResult Activacao()
        {
            ViewBag.Departamentos = new SelectList(db.Departamentos.ToList(), "DepartamentoId", "Nome_Departamento");
            return View();
        }

        [HttpPost]
        public IActionResult GetDisciplina(int Id)
        {
            var discplinas  = db.TesteDisciplinas.Include(e => e.Disciplina).Where(e => e.CursoId == Id).ToList()
                .Select(x=> new {
                    Id = x.Disciplina.DisciplinaId,
                    Disciplina = x.Disciplina.Nome_Disciplina
            });

            ViewBag.Disciplinas = new SelectList(discplinas, "Id", "Disciplina");

            return PartialView("_DropDownDisciplina");
        }

    }
}

