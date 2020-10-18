using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Data.Entidades
{
    public class tb_Agendamento_Teste
    {
        [Key]
        public int AgendamentoTesteId { get; set; }
        [Required]
        public DateTime Data_Realizacao { get; set; }
        [Required]
        public DateTime Data_Exp { get; set; }
        [Required]
        [MaxLength(10)]
        public string Horario { get; set; }
        public bool Estado { get; set; } 
        public int DisciplinaId { get; set; }
        public virtual tb_Disciplina Disciplina { get; set; }
        public int CursoId { get; set; }
        public virtual tb_Curso Curso { get; set; }
        public ICollection<tb_Teste> Testes { get; set; }
        public int SalaExameId { get; set; }
        public virtual tb_Sala_Exame SalaExame { get; set; }

    }
}
