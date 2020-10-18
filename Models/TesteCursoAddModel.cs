using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Models
{
    public class TesteCursoAddModel
    {
        [Required(ErrorMessage = "Selecione o departamento")]
        public int? Id_Departamento { get; set; }
        [Display(Name = " Descrição Curso")]
        [Required(ErrorMessage = "Selecione o curso")]
        public int? Id_Curso { get; set; }
        [Display(Name = " Descrição Disciplina")]
        [Required(ErrorMessage = "Selecione a disciplina")]
        public int? Id_Disciplina { get; set; }

        [Display(Name = " Estado")]
        [Required(ErrorMessage = "Selecione o estado")]
        public bool? Estado { get; set; }

        [Display(Name = " Duração do Teste (Min)")]
        [Required(ErrorMessage = "Digite a duração")]
        public string Duracao { get; set; }

        [Display(Name = "Perguntas (Total)")]
        [Required(ErrorMessage = "Digite o total de perguntas")]
        public string Perguntas { get; set; }
    }
}
