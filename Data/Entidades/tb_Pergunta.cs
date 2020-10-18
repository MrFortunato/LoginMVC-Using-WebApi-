using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exame_Online.Data.Entidades
{
    public class tb_Pergunta
    {
        [Key]
        [Display(Name ="Nrº")]
        public int PerguntaId { get; set; }
        [Display(Name = "Descrição da Pergunta")]

        [Required]
        public string Descricao { get; set; }

        [Display(Name = "Cotação da Pergunta")]
        [Column(TypeName = "decimal(4,2)")]
        public decimal Valor { get; set; }

        [Display(Name = "Tipo de Pergunta")]
        public int TipoPerguntaId { get; set; }
        public virtual tb_TipoPergunta TipoPergunta {get;set;}

        [Display(Name = "Disciplina")]
        public int DisciplinaId { get; set; }
        public virtual tb_Disciplina Disciplina { get; set; }
        [Display(Name = "Imagem")]
        [MaxLength(255)]
        public string UrlImagem { get; set; }
        public virtual ICollection<tb_Teste_Pergunta> TestePerguntas { get; set; }
        public virtual ICollection<tb_Resposta> Respostas { get; set; }

       
    }
}
