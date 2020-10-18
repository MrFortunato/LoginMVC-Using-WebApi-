using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exame_Online.Data.Entidades
{
    public class tb_Sala_Exame
    {
        [Key]
        public int SalaExameId { get; set; }

        [MaxLength(10)]
        [Required]
        public string Descricao { get; set; }
        [Required]
        [Range(1,100)]
        public int Lotacao { get; set; }
        public virtual ICollection<tb_Agendamento_Teste> AgendamentoTestes { get; set; }
    }
}
