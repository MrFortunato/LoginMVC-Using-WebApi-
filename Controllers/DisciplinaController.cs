using Exame_Online.Data.Contexto;
using Exame_Online.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exame_Online.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly Contexto db;

        public DisciplinaController(Contexto _db)
        {
            db = _db;
        }
        public IActionResult Lista()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar([Bind("Nome_Disciplina")] DisciplinaAddModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri("https://localhost:44308/api/");
                        var response = await client.PostAsync("Disciplina/Cadastrar", content);
                        if (response.IsSuccessStatusCode)
                        {
                            ViewData["result"] = "1";
                            return View();
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

        [HttpGet]
        public async Task<IActionResult> Editar(int? Id)
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }
                DisciplinaAddModel disciplina = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44308/api/");

                    var result = await client.GetAsync($"Disciplina/Selecionar/{Id}");

                    if (result.IsSuccessStatusCode)
                    {
                        disciplina = await result.Content.ReadAsAsync<DisciplinaAddModel>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }

                if (disciplina == null)
                {
                    return NotFound();
                }
                return View(disciplina);

            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Editar([Bind("Id_Disciplina,Nome_Disciplina")] DisciplinaAddModel model)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44308/api/");
                        var response = await client.PutAsJsonAsync("Disciplina/Editar", model);
                        if (response.IsSuccessStatusCode)
                        {
                            ViewData["result"] = "1";
                            return View();
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }
                }
                return View(model);
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }
            //model.Departamentos = db.Departamentos.ToList();
            return View(model);

        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int? Id)
        {

            if (Id != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44308/api/");

                    var response = await client.DeleteAsync($"Disciplina/Deletar/{Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Json("1");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }

            }
            return Json("2");
        }       
     
    }
}
