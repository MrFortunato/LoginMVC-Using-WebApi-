using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Data.Entidades
{
    public class tb_TesteDisciplina
    {

        public int CursoId { get; set; }
        public virtual tb_Curso Curso { get; set; }
        public int DisciplinaId { get; set; }
        public virtual tb_Disciplina Disciplina { get; set; }
        [Required]
        public int Duracao { get; set; }
        public int Perguntas { get; set; }
        public bool Estado { get; set; }
    }
}
