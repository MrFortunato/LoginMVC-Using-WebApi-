using Exame_Online.Data.Contexto;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Exame_Online.Data.Entidades.IdentyModel;
using static Exame_Online.ModelsView.PerguntaView;

namespace Exame_Online.Controllers
{

    public class CandidatoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly Contexto db;
        public CandidatoController(Contexto _db, UserManager<Usuario> userManager)
        {
            db = _db;
            _userManager = userManager;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Perfil()
        {
            CandidatoViewModel Candidato = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44308/api/Candidato/Perfil");

                var result = await client.GetAsync("https://localhost:44308/api/Candidato/Perfil");

                if (result.IsSuccessStatusCode)
                {
                    Candidato = await result.Content.ReadAsAsync<CandidatoViewModel>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (Candidato == null)
            {
                return NotFound();
            }

            return View(Candidato);
        }
        // Url:/Candidato/Candidaturas
        

        [HttpGet]
        public IActionResult Correcao(int? Id ,int? Candidato)
        {
            try
            {
                var model = db.TestePerguntas
                          .Include(x => x.Pergunta)
                          .ThenInclude(x => x.TipoPergunta)
                          .Include(x => x.Teste)
                          .ThenInclude(x => x.Disciplina)
                          .Where(x => x.Teste.TesteId == Id
                          && x.Teste.CandidaturaId == Candidato).Select(x => new PerguntaView
                          {
                              
                              Id_Pergunta = x.TestePerguntaId,
                              Id_Teste = x.TesteId,
                              Pergunta_Nr = x.Nr_Pergunta,
                              Pergunta = x.Pergunta.Descricao,
                              TipoPergunta = x.Pergunta.TipoPergunta.Tipo_Pergunta,
                              Disciplina = x.Pergunta.Disciplina.Nome_Disciplina,
                              Valor = x.Pergunta.Valor,
                              Opcoes = db.Respostas.Include(x => x.Pergunta)
                            .ThenInclude(x => x.TipoPergunta)
                        .Where(e => e.Estado == true && e.PerguntaId == x.PerguntaId)
                              .Select(r => new RespostaView
                              {
                                  TipoPergunta = r.Pergunta.TipoPergunta.Tipo_Pergunta,
                                  Id_Opcao = r.RespostaId,
                                  RespostaSistema = r.Resposta_Desc, 
                                 
                              }).ToList()
                          })
                          .ToList();

                

                foreach (var item in model)
                {
                    // Verificar se a resposta já está registrada na base de dados
                    var respostas = db.TesteResultados
                        .Include(x => x.Resposta).Where(x => x.TesteId == item.Id_Teste
                         && x.TestePerguntaId == item.Id_Pergunta
                         && x.Resposta != null
                         && x.Resposta.Estado == true)
                        .Select(x => new
                        {
                            Id = x.RespostaId,
                            Res = x.RespostaTexte,
                            Respostass = x.Resposta.Resposta_Desc,
                            cotacao = x.Cotacao

                        })
                        .ToList();

                    foreach(var e in respostas)
                    {
                        item.Opcoes.Where(x => x.Id_Opcao == e.Id).ToList().FirstOrDefault().RespostaTexte = e.Res;
                        item.Opcoes.Where(x => x.Id_Opcao == e.Id).ToList().FirstOrDefault().RespostaSistema = e.Respostass;
                        item.Opcoes.Where(x => x.Id_Opcao == e.Id).ToList().FirstOrDefault().Selecionado = Convert.ToString(e.Id);
                        item.Opcoes.Where(x => x.Id_Opcao == e.Id).ToList().FirstOrDefault().cotacao = e.cotacao;
                    }

                }

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri("https://localhost:44308/api/Candidato/");
                        var response = await client.PostAsync("Login", content);
                        if (response.IsSuccessStatusCode)
                        {
                            ViewData["result"] = "1";
                            return RedirectToAction("Perfil");
                        }
                        else
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            ModelState.AddModelError(string.Empty, responseString);
                        }
                    }
                }
                return View(model);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }
            return View(model);

        }

        
    }

}
