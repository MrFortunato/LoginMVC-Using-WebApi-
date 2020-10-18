using Exame_Online.Data.Contexto;
using Exame_Online.Models;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exame_Online.Controllers
{
    public class CursoController : Controller
    {
        //private readonly IUnitOfWork _uow;

        private readonly Contexto db;


        public CursoController(Contexto _db)
        {

            db = _db;
        }
  
        [HttpGet]
        public IActionResult Lista()
        {

            return View();
        }

        //
        [HttpGet]
        public IActionResult Testes()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");
            return View();
        }

        private List<DepartamentoModelView> DropListas()
        {
            List<DepartamentoModelView> departamentos = new List<DepartamentoModelView>();
            var Depart = db.Departamentos.ToList();
            foreach (var item in Depart)
            {
                departamentos.Add(new DepartamentoModelView
                {
                    Id_Departamento = item.DepartamentoId,
                    Nome_Departamento = item.Nome_Departamento
                });
            }
            return departamentos;
        }

        private async Task<IEnumerable<DepartamentoModelView>> Departamentos()
        {
            List<DepartamentoModelView> departamentos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44308/api/");

                var result = await client.GetAsync("Departamento/GetAll");

                if (result.IsSuccessStatusCode)
                {
                    departamentos = await result.Content.ReadAsAsync<List<DepartamentoModelView>>();
                }
                else
                {
                    departamentos = new List<DepartamentoModelView>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return departamentos.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([Bind("Id_Departamento,Nome_Curso")] CursoAddModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri("https://localhost:44308/api/");
                        var response = await client.PostAsync("Curso/Cadastrar", content);
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
                ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");
                return View(model);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }
            ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");
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
                CursoAddModel Curso = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44308/api/");

                    var result = await client.GetAsync($"Curso/Selecionar/{Id}");

                    if (result.IsSuccessStatusCode)
                    {
                        Curso = await result.Content.ReadAsAsync<CursoAddModel>();
                    }
                    else
                    {
                        ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }

                if (Curso == null)
                {
                    return NotFound();
                }
                ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");
                return View(Curso);

            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }
            //model.Departamentos = db.Departamentos.ToList();
            return View(/*model*/);
        }

        [HttpPost]
        public async Task<IActionResult> Editar([Bind("Id_Curso,Nome_Curso, Id_Departamento")] CursoAddModel model)
        {
            model.Departamentos = db.Departamentos.ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44308/api/");
                        var response = await client.PutAsJsonAsync("Curso/Editar", model);
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

                    var response = await client.DeleteAsync($"Curso/Deletar/{Id}");
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
