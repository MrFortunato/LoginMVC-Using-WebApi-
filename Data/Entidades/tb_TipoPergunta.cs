using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Data.Entidades
{
    public class tb_TipoPergunta
    {
        [Key]
        [Display(Name = "Nrº")]
        public int TipoPerguntaId { get; set; }

        [Display(Name = "Tipo de Pergunta")]
        [MaxLength(50)]
        public string Tipo_Pergunta { get; set; }
        public ICollection<tb_Pergunta>  Perguntas { get; set; }
    }
}
