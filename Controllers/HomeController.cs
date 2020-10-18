using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Exame_Online.Models;
using Exame_Online.ModelsView;
using Exame_Online.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Exame_Online.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto db;

        public HomeController(ILogger<HomeController> logger, Contexto _db)
        {
            _logger = logger;
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //Url:/Home/Painel
        [HttpGet]
        public PainelViewModel Painel()
        {
            var Dados = new PainelViewModel
            {
                TotalCandidatos = db.Candidaturas.Where(x => x.Estado == true).Count(),
                TotalDisciplinas = db.Disciplinas.Count(),
                TotalCursos = db.Cursos.Count(),
                TotalEx_Agendados = db.Testes
                .Include(x => x.AgendamentoTeste)
                .Where(x => x.AgendamentoTesteId != null
                && x.AgendamentoTeste.Data_Realizacao > DateTime.Now).Count(),
                TotalEx_Cancelados = db.Testes.Where(x => x.Estado == false).Count()

            };
            return Dados;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
