using Exame_Online.Data.Contexto;
using Exame_Online.Data.Entidades;
using Exame_Online.Models;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Exame_Online.Data.Entidades.IdentyModel;

namespace Exame_Online.Controllers
{

    public class InscricaoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInUsuario;
        private IWebHostEnvironment _en;
        private readonly Contexto db;

        public InscricaoController(Contexto _db, IWebHostEnvironment en,
                                 UserManager<Usuario> userManager,
                                 SignInManager<Usuario> signInUsuario)
        {
            db = _db;
            _en = en;
            _userManager = userManager;
            _signInUsuario = signInUsuario;
        }
        [HttpGet]
        public IActionResult Lista()
        {       
            return View();
        }
        [HttpGet]
        public async Task<List<InscricaoViewModel>> Todos()
        {
            List<InscricaoViewModel> Lista = new List<InscricaoViewModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44308/api/Inscricao/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Lista = JsonConvert.DeserializeObject<List<InscricaoViewModel>>(apiResponse);
                }
            }

            return Lista;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Online()
        {
            Drops();//Método para carregar as dropdowns
            return View();
        }
        private async Task<List<DepartamentoModelView>> DropDownDepartamentos()
        {
            List<DepartamentoModelView> Lista = new List<DepartamentoModelView>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44308/api/Departamento/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Lista = JsonConvert.DeserializeObject<List<DepartamentoModelView>>(apiResponse);
                }
            }

            return Lista;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Online(CandidaturaAddModel model)
        {
            // Função para copiar a Foto na pasta = Imagens
            //string Imagem = UrlImagem(model);
            //List<int> years = new List<int>();

            if (ModelState.IsValid)
            {

                try
                {
                    
                        using (var _httpClient = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                            _httpClient.BaseAddress = new Uri("https://localhost:44308/api/Inscricao/");

                            var response = await _httpClient.PostAsync("Registrar", content);
                         
                            if (response.IsSuccessStatusCode)
                            {
                                
                                ViewData["result"] = "1";
                                ModelState.Clear();
                                Drops();
                                return View();
                            }
                            else
                            {
                                Drops();
                                ViewData["result"] = response.Content.ReadAsStringAsync().Result; //Resposta Http da Api
                                return View();
                             
                            }
                        }
                
                }
                    catch (Exception){ 
                    Drops();
                    ViewData["result"] = "Erro";
                    ModelState.AddModelError("", "Erro no cadastramento");
                }
            }
            Drops();
            return View(model);

        }

        //Metodo para Carregar a DropdownList a partir do PartialView
        [HttpPost]
        public IActionResult GetCurso(int Id)
        {
            List<tb_Curso> Cursos = db.Cursos.Where(e => e.DepartamentoId == Id).ToList();

            ViewBag.Cursos = new SelectList(Cursos, "CursoId", "Nome_Curso");

            return PartialView("_DropdownCursos");

        }
        
        //Método para carregar as dropdowns
        private void Drops()
        {
            List<int> years = new List<int>();
            ViewBag.Departamentos = new SelectList(db.Departamentos.ToList(), "DepartamentoId", "Nome_Departamento");

            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 2; i++)
            {
                years.Add(i);
            }
            ViewBag.Anos = new SelectList(years);
        }

        // Função para arquivar imagem Imagem 
        //private string UrlImagem(CandidaturaAddModel model)
        //{
        //    string UniqueNome = null;

        //    if (model.Foto != null)
        //    {
        //        var ImgDestino = Path.Combine(_en.WebRootPath, "Imagens");
        //        UniqueNome = Guid.NewGuid().ToString().Substring(0, 6) + "_" + model.Foto.FileName;
        //        string filePath = Path.Combine(ImgDestino, UniqueNome);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.Foto.CopyTo(fileStream);
        //        }
        //    }
        //    else
        //    {
        //        UniqueNome = "user.jpg";
        //    }

        //    return UniqueNome;
        //}

        //public int AddTeste(int Id_Candidato)
        //{
        //    int resultado = 0;
        //    //Selecionado o candidato
        //    var c = db.Candidaturas.FirstOrDefault(x => x.CandidaturaId == Id_Candidato);

        //    //if(c != null)
        //    //{

        //    //}
        //    var op = db.opcoesCandidaturas.FirstOrDefault(x => x.CandidaturaId == c.CandidaturaId && x.Opcao == "1");

        //    // Instanciando a biblioteca para a gerar a lista de pergunta aleatória
        //    var ran = new Random();

        //    // Selecionando o número de disciplinas associada a curso em que o candidato inscreveu-se
        //    var disciplinas = db.TesteDisciplinas.Where(x => x.CursoId == op.CursoId).ToList();

        //    // Enquanto existir cursos associados uma ou mais disciplinas
        //    foreach (var d in disciplinas)
        //    {

        //        var test = new tb_Teste
        //        {
        //            CandidaturaId = c.CandidaturaId,
        //            DisciplinaId = d.DisciplinaId,
        //            Dt_Realizacao = DateTime.Now,
        //            Estado = true,
        //            Duracacao = d.Duracao, // Duração do Teste Pega o Tempo do TesteDisciplina

        //        };
        //        db.Testes.Add(test);
        //        db.SaveChanges();


        //        //Selecionado 3 pergundas aleatoria da disciplina  Selecionado 
        //        var lista = db.Perguntas.Where(x => x.DisciplinaId == d.DisciplinaId).ToList().OrderBy(x => ran.Next()).Take(d.Perguntas);

        //        var teste = db.Testes.Where(x => x.CandidaturaId == c.CandidaturaId && x.DisciplinaId == d.DisciplinaId).FirstOrDefault();

        //        foreach (var item in lista)
        //        {
        //            int OrderPergunta = db.TestePerguntas.Where(x => x.TesteId == teste.TesteId).Count();
        //            var testPergunta = new tb_Teste_Pergunta
        //            {
        //                TesteId = teste.TesteId,
        //                Estado = true,
        //                Nr_Pergunta = OrderPergunta + 1,
        //                PerguntaId = item.PerguntaId

        //            };
        //            db.TestePerguntas.Add(testPergunta);
        //            resultado = db.SaveChanges();
        //        }
        //    }
        //    return resultado; ;
        //}
    }
}
