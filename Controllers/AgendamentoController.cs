using Exame_Online.Data.Contexto;
using Exame_Online.Data.Entidades;
using Exame_Online.Models;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exame_Online.Controllers
{
    [Authorize]
    public class AgendamentoController : Controller
    {
        private readonly Contexto db;

        public AgendamentoController(Contexto _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult Todos()
        {
    
            return View();
        }
      
        // Url:/Agendamento/Cadastrar
        [HttpGet]
        public IActionResult Cadastrar()
        {
            List<tb_Departamento> departamentos = db.Departamentos.ToList();
            List<tb_Sala_Exame> Salas = db.Sala_Exames.ToList();
            ViewBag.Salas = new SelectList(Salas, "SalaExameId", "Descricao");
            ViewBag.Departamentos = new SelectList(departamentos, "DepartamentoId", "Nome_Departamento");
            var Agendamento = new AgendamentoAddModel();
            return PartialView("_AddEdit", Agendamento);
        }

        [HttpGet]
        public JsonResult ListarCursoDepart(int? Id)
        {
            var Cursos =new  List<CursoViewModel>();
            if(Id == null)
            {
                return Json("3");
            }
            var C = db.Cursos
                .Where(x => x.DepartamentoId == Id)
                .Include(x => x.Departamento);

            if(C== null)
            {
                return Json("2");
            }

            foreach(var item in C)
            {
                Cursos.Add(new CursoViewModel 
                {
                    Id_Curso = item.CursoId,
                    Nome_Curso = item.Nome_Curso
                });;
            };

            return Json(Cursos); ;
        }

        [HttpGet]
        public JsonResult ListarDisciplinaCurso(int? Id)
        {
            var Disciplinas = new List<DiscplinaViewModel>();
            if (Id == null)
            {
                return Json("3");
            }
            var D = db.TesteDisciplinas
                .Where(x => x.CursoId == Id)
                .Include(x => x.Disciplina);

            if (D == null)
            {
                return Json("2");
            }

            foreach (var item in D)
            {
                Disciplinas.Add(new DiscplinaViewModel
                {
                    Id_Disciplina = item.Disciplina.DisciplinaId,
                    Nome_Disciplina = item.Disciplina.Nome_Disciplina
                }); ;
            };

            return Json(Disciplinas);
        }

        //Url:/Agendamento/Cadastrar
        [HttpPost]
        public JsonResult Cadastrar(AgendamentoAddModel model)
        {

               
                var Agendamento = db.AgendamentosTestes
                    .Where(x => x.CursoId == model.Id_Curso
                    && x.Data_Realizacao == Convert.ToDateTime(model.Dt_Realizacao)
                    && x.Horario == model.Horario).FirstOrDefault();

                if(Agendamento!= null)
                {
                    return Json("2");
                }

                var age = new tb_Agendamento_Teste
                {
                    DisciplinaId = model.Id_Disciplina,
                    CursoId = model.Id_Curso,
                    Data_Realizacao = Convert.ToDateTime(model.Dt_Realizacao),
                    Data_Exp = Convert.ToDateTime(model.Dt_expirar), 
                    Estado = true,
                    Horario = model.Horario,
                    SalaExameId = model.Id_SalaExame
                };
                db.AgendamentosTestes.Add(age);
                db.SaveChanges();

                return Json("1"); 
        }

      


    }
}
