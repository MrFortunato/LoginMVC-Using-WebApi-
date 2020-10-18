using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exame_Online.Data.Entidades
{
    public class tb_Curso
    {
        [Key]
        public int CursoId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Nome_Curso { get; set; }
        public int DepartamentoId { get; set; }
        public virtual tb_Departamento Departamento { get; set; }
        public virtual ICollection<tb_Agendamento_Teste> AgendamentoTestes { get; set; }
        public virtual ICollection<tb_Opcoes_Candidatura> OpcoesCandidaturas { get; set; }
    }
}
