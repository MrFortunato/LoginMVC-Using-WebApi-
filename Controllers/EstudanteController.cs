using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Exame_Online.Controllers
{
    public class EstudanteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //Metodo para nova candidatura
        [HttpGet]
        public IActionResult Inscricao()
        {
            return View();
        }
    }
}
