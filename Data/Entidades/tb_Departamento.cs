using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Data.Entidades
{
    public class tb_Departamento
    {
        [Key]
        [Display(Name = "ID")]
        public int DepartamentoId { get; set; }
        [Display(Name ="Departamento")]
        [Required(ErrorMessage ="Digite o departamento")]
        [MaxLength(50)]
        public string Nome_Departamento { get; set; }
        public virtual ICollection<tb_Curso> Cursos { get; set; }
    }
}
