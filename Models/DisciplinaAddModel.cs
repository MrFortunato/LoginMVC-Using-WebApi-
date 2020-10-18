using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Models
{
    public class DisciplinaAddModel
    {
        [Display(Name="ID")]
        public int Id_Disciplina { get; set; }
     
        [Display(Name = "Descrição Disciplina")]
        [StringLength(20, ErrorMessage = "No máximo 20 caracteres")]
        [Required(ErrorMessage = "Digite a disciplina")]
        public string Nome_Disciplina { get; set; }
        public int Id_Curso { get; set; }
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Digite números de perguntas")]
        public int Perguntas { get; set; }

        [Required(ErrorMessage = "Digite a ducação do texte")]
        public int Duracao { get; set; }
    }
}
