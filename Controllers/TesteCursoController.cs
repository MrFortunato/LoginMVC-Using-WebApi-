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
    public class TesteCursoController : Controller
    {
        private readonly Contexto db;

        public TesteCursoController(Contexto _db)
        {
            db = _db;
        }
        //Url:TesteCurso/Lista
        [HttpGet]
        public IActionResult Lista()
        {
            return View();
        }
        //Url:TesteCurso/Cadastrar
        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");

            return View();
        }
        //Url: TesteCurso/Cadastrar
        [HttpPost]
        public async Task<IActionResult> Cadastrar(TesteCursoAddModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri("https://localhost:44308/api/");
                        var response = await client.PostAsync("TesteCurso/Cadastrar", content);
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
            catch (DbUpdateException )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }
            return View(model);
        }
       
   

        //Url:TesteCurso/Editar/
        [HttpGet]
        public async Task<IActionResult> Editar(int Id_Disciplina, int Id_Curso)
        {
            try
            {
                if (Id_Disciplina <= 0 && Id_Curso <= 0)
                {
                    return BadRequest();
                }
                TesteCursoAddModel teste = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44308/api/");

                    var result = await client.GetAsync($"TesteCurso/Adicionar/{Id_Disciplina}/{Id_Curso}");

                    if (result.IsSuccessStatusCode)
                    {
                        teste = await result.Content.ReadAsAsync<TesteCursoAddModel>();
                    }
                    else
                    {
                        ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }

                if (teste == null)
                {
                    return NotFound();
                }
                ViewBag.Departamentos = new SelectList(DropListas(), "Id_Departamento", "Nome_Departamento");
                return View(teste);

            }
            catch (DbUpdateException /* ex */)
            { 
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }

            return View(/*model*/);
        }
        //Url:TesteCurso/Editar/
        [HttpPost]
        public async Task<IActionResult> Editar(TesteCursoAddModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44308/api/");
                        var response = await client.PutAsJsonAsync("TesteCurso/Editar", model);
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
                ModelState.AddModelError("", "Não foi possível salvar as alterações. " +
                 "Tente novamente e se o problema persistir, " +
                 "consulte o administrador do sistema.");
            }
            return View(/*model*/);
        }

        //Url:TesteCurso/DropCurso/1
        [HttpGet]
        public JsonResult DropCurso(int Id)
        {
            List<CursoViewModel> Lista = new List<CursoViewModel>();
            var Cursos = db.Cursos
                .Where(x => x.DepartamentoId == Id)
                .ToList();
            foreach (var item in Cursos)
            {
                Lista.Add(new CursoViewModel
                {
                    Id_Curso = item.CursoId,
                    Nome_Curso = item.Nome_Curso
                });
            }
            return Json(Lista);
        }
        //Url:TesteCurso/DropCurso/1
        [HttpGet]
        public JsonResult DropDisciplina()
        {
            List<DisciplinaAddModel> disciplinas = new List<DisciplinaAddModel>();
            var resultado = db.Disciplinas.ToList();
            foreach (var item in resultado)
            {
                disciplinas.Add(new DisciplinaAddModel
                {
                    Id_Disciplina = item.DisciplinaId,
                    Nome_Disciplina = item.Nome_Disciplina
                });
            }
            return Json(disciplinas);
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

    }
}
