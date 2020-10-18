using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Models
{
    public class DepartamentoAddModel
    {
        [Display(Name = "ID")]
        public int Id_Departamento { get; set; }
       
        [Display(Name = "Descrição Departamento")]
        //[StringLength(20, ErrorMessage = "No máximo 20 caracteres")]
        //[Required(ErrorMessage = "Digite o Departamento")]
        public string Nome_Departamento { get; set; }


    }
}
