using Exame_Online.Data.Entidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exame_Online.Models
{
    public class CursoAddModel
    {
        public int Id_Curso { get; set; }

        [Display(Name ="Curso")]
        //[StringLength(30,ErrorMessage ="No máximo 30 caracteres")]
        [Required(ErrorMessage ="Digite o curso")]
        public string  Nome_Curso { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Selecione o Departamento")]
        public int? Id_Departamento { get; set; }

        [NotMapped]
        public List<tb_Departamento> Departamentos { get; set; }

    
    }
}
