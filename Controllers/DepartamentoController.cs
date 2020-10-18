using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exame_Online.Data.Contexto;
using Exame_Online.Data.Entidades;
using Exame_Online.Models;
using Exame_Online.ModelsView;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Threading.Tasks;


namespace Exame_Online.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly Contexto db;
      
        public DepartamentoController(Contexto _db)
        {
            db = _db;
        }

        [HttpGet]
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
        public async Task<IActionResult> Cadastrar(DepartamentoAddModel model)
        {
            //if (ModelState.IsValid)
            //{
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    client.BaseAddress = new Uri("https://localhost:44308/api/Departamento/Cadastrar");

                    var response = await client.PostAsync("Cadastrar", content);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["result"] = "1";
                        ModelState.Clear();
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            //}
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> Editar(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            DepartamentoAddModel Departamento = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44308/api/");

                var result = await client.GetAsync($"Departamento/Selecionar/{Id}");

                if (result.IsSuccessStatusCode)
                {
                    Departamento = await result.Content.ReadAsAsync<DepartamentoAddModel>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (Departamento == null)
            {
                return NotFound();
            }
            return View(Departamento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind("Id_Departamento, Nome_Departamento")] DepartamentoAddModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44308/api/");
                        var response = await client.PutAsJsonAsync("Departamento/Editar", model);
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
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o Administrador do Sistema.");
            }
            return View(model);
        }

        [HttpDelete]
        public async Task<ActionResult> Deletar(int? Id)
        {

            if (Id != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44308/api/");

                    var response = await client.DeleteAsync($"Departamento/Deletar/{Id}");
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
