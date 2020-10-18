using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Models
{
    public class OpcaoRespostaAddModel
    {
        [Display(Name = "ID")]
        public int Id_OpcaoResposta { get; set; }

        [Display(Name = "Resposta")]
        public string Resposta { get; set; }

        [Display(Name = "Cotação")]
        public decimal ValorR { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }
       


    }
}
