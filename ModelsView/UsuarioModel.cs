using System.ComponentModel.DataAnnotations;

namespace Exame_Online.ModelsView
{
    public class UsuarioModel
    {
        [Display(Name="Email")]
        [Required(ErrorMessage ="Digite o email")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Digite a Senha")]
        public string Password { get; set; }

        public bool Relembrar { get; set; }
    }
}
