using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exame_Online.Data.Entidades
{
    public class tb_Teste_Pergunta
    {
        [Key]
        public int TestePerguntaId { get; set; }
        public int TesteId { get; set; }
        public virtual tb_Teste Teste { get; set; }  
        public int PerguntaId { get; set; }
        public virtual tb_Pergunta Pergunta { get; set; }
        [Required]
        [Range(1,100)]
        public int Nr_Pergunta { get; set; }
        [Required]
        public bool Estado { get; set; }   
           
    }
}
