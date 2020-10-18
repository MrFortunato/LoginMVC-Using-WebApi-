using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Data.Entidades
{
    public class tb_Disciplina
    {
        [Key]
        public int DisciplinaId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nome_Disciplina { get; set; }
        public virtual ICollection<tb_Teste> Testes { get; set; }

        public virtual ICollection<tb_Agendamento_Teste> AgendamentosTestes { get; set; }
    }
}
