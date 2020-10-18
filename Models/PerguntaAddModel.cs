using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Models
{
    public class PerguntaAddModel
    {
        [Key]
        [Display(Name = "ID")]
        public int Id_Pergunta { get; set; }
        [Display(Name ="Pergunta")]
        public string Questao { get; set; }
        [Display(Name = "Cotação")]
        public decimal ValorP { get; set; }

        [Display(Name = "Disciplina")]
        public int Id_Disciplina { get; set; }

        [Display(Name = "Tipo de Pergunta")]
        public int Id_TipoPergunta { get; set; }

        [Display(Name = "Imagem")]
        public FormFile Imagem { get; set; }

        public List<OpcaoRespostaAddModel> respostaAddModel { get; set; }

    }
    public class AdicionarRequestDto
    {
        public PerguntaAddModel Pergunta { get; set; }
        public List<OpcaoRespostaAddModel> Resposta { get; set; }
    }
}
