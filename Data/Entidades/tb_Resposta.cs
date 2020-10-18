using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exame_Online.Data.Entidades
{
    public class tb_Resposta
    {
        [Key]
        public int RespostaId { get; set; }

        [Required]
        public string Resposta_Desc { get; set; }
        public int PerguntaId { get; set; }
        public virtual tb_Pergunta Pergunta { get; set; }

        [Required]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Valor { get; set; }
        public bool Estado { get; set; } 
        public ICollection <tb_Teste_Resultado> TesteResultados { get; set; }


    }
}
