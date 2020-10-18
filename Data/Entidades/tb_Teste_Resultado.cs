using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exame_Online.Data.Entidades
{
    public class tb_Teste_Resultado
    {
        [Key]
        public int TesteResultadoId { get; set; }
        public int TesteId { get; set; }
        public virtual tb_Teste Teste { get; set; }
        public int TestePerguntaId { get; set; }
        public virtual tb_Teste_Pergunta TestePergunta { get; set; }
        public int RespostaId { get; set; }
        public virtual tb_Resposta Resposta { get; set; }
        [MaxLength(50)]
        public string RespostaTexte { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Cotacao { get; set; }
       
       
      
    }
}
