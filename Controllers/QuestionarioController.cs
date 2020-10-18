using Exame_Online.Data.Contexto;
using Exame_Online.Models;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exame_Online.Controllers
{

    [Authorize]
    public class QuestionarioController : Controller
    {
        private readonly Contexto db;
       
        public QuestionarioController(Contexto _db)
        {
            db = _db;
        }
       
        [HttpGet]
        public IActionResult Lista()
        {
            return View();
        }
        //Url:Questionar/Todos
        [HttpGet]
        public async Task<List<PerguntaModelView>> Todos()
        {
            List<PerguntaModelView> Lista = new List<PerguntaModelView>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44308/api/Questionario/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Lista = JsonConvert.DeserializeObject<List<PerguntaModelView>>(apiResponse);
                }
            }
            return Lista;
        }
        //Url:Questionar/Adicionar
        [HttpGet]
        public IActionResult Adicionar()
        {
            ViewBag.Disciplinas = new SelectList(db.Disciplinas.ToList(), "DisciplinaId", "Nome_Disciplina");
            ViewBag.TipoPergunta = new SelectList(db.TipoPerguntas.ToList(), "TipoPerguntaId", "Tipo_Pergunta");
            return View();
        }
        //Url:Questionar/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(PerguntaAddModel pergunta, OpcaoRespostaAddModel[] resposta)
        {
            using (var _httpClient = new HttpClient())
            {      
                AdicionarRequestDto paramAdd = new AdicionarRequestDto();
                paramAdd.Pergunta = new PerguntaAddModel
                {
                    Id_Pergunta = pergunta.Id_Pergunta,
                    Questao = pergunta.Questao,
                    Id_Disciplina = pergunta.Id_Disciplina,
                    Id_TipoPergunta = pergunta.Id_TipoPergunta,
                    Imagem = pergunta.Imagem,
                    ValorP = pergunta.ValorP,
                };

                paramAdd.Resposta = new List<OpcaoRespostaAddModel>();

                foreach (var item in resposta)
                {
                    paramAdd.Resposta.Add(new OpcaoRespostaAddModel 
                    {
                        Id_OpcaoResposta = item.Id_OpcaoResposta,
                        Resposta = item.Resposta,
                        Estado = item.Estado,
                        ValorR = item.ValorR,

                    });    

                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(paramAdd), Encoding.UTF8, "application/json");
                             
                _httpClient.BaseAddress = new Uri("https://localhost:44308/api/Questionario/");

                var response = await _httpClient.PostAsync("Adicionar", content);
                if (response.IsSuccessStatusCode) //Sucesso
                {         
                    return Json("1");
                }
                else
                {                   
                    var RespostaHttp = response.Content.ReadAsStringAsync().Result; //Resposta Http da Api
                    return Json(RespostaHttp);
                }
            }
     
        }

      
    }
}
