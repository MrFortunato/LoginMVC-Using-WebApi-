using Exame_Online.Data.Contexto;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Exame_Online.Data.Entidades.IdentyModel;

namespace Exame_Online.Controllers
{
    [Authorize]
    public class UtangaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInUsuario;
        private readonly Contexto db;
        private IWebHostEnvironment _en;
        public UtangaController(Contexto _db,
                                 UserManager<Usuario> userManager,
                                 SignInManager<Usuario> signInUsuario,
                                 IWebHostEnvironment en)
        {
            db = _db;
            _userManager = userManager;
            _signInUsuario = signInUsuario;
            _en = en;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
        
            return View();
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var pessoa = db.DadosPessoais.Include(x => x.Candidaturas).FirstOrDefault(x => x.UsuarioId == Convert.ToInt32(UserId));
                if (User.IsInRole("Candidato"))
                {


                    if (pessoa != null)
                    {
                        return RedirectToAction("Perfil", "Candidato");
                    }

                    return RedirectToAction("Conta", "Professor");
                }
                if (User.IsInRole("Administrador"))
                {
                    if(pessoa != null)
                    {
                        //ClaimsIdentity identity = new ClaimsIdentity()
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Conta", "Professor");
                }

            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Login(UsuarioModel model)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            if (!string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Password))
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                var resultado = await _signInUsuario.PasswordSignInAsync(model.Email, model.Password,
                model.Relembrar, lockoutOnFailure: false);

                if (resultado.Succeeded)
                {

                    return RedirectToAction("Index", "Utanga");

                }

            }
            // Se a operaação chegar aqui... não houve sucesso
            ModelState.AddModelError(nameof(model.Email), "Acesso Negado: Email ou senha inválida!!!");
            // Se a operaação chegar aqui... não houve sucesso
            return View(model);

        }

        //Metodo para visualizão da imagem do usuario logado no Menu
        [HttpGet]
        [Authorize]
        public IActionResult UserImagem()
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // U
            var pessoa = db.DadosPessoais.FirstOrDefault(x => x.UsuarioId == Convert.ToInt32(UserId));
            string fileName = null;
            byte[] imageData = null;

            if (User.Identity.IsAuthenticated)
            {
                if (pessoa == null)
                {
                    return null;
                }

                if (pessoa.UrlFoto == null)
                {
                    pessoa.UrlFoto = "user.jpg";
                    fileName = Path.Combine(_en.WebRootPath + "\\Imagens\\" + pessoa.UrlFoto);
                    imageData = System.IO.File.ReadAllBytes(fileName);
                    return File(imageData, "image/png");
                }
                else
                {
                    fileName = Path.Combine(_en.WebRootPath + "\\Imagens\\" + pessoa.UrlFoto);
                    imageData = System.IO.File.ReadAllBytes(fileName);
                    return File(imageData, "image/png");
                }
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
                await _signInUsuario.SignOutAsync(); // <-- with this one 

            return RedirectToAction("Login");
        }

    
    }
}
