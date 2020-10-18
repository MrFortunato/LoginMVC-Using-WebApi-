using System.ComponentModel.DataAnnotations;

namespace Exame_Online.ModelsView
{
    public class DepartamentoModelView
    {
        [Display(Name ="ID")]
        public int Id_Departamento { get; set; }
        [Display(Name = "Descrição Departamento")]
        public string Nome_Departamento { get; set; }

        [Display(Name = "Total Cursos")]
        public int Total_Curso { get; set; }
    }
}
