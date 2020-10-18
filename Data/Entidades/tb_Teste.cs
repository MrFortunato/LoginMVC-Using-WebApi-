using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Data.Entidades
{
    public class tb_Teste
    {
        [Key]
        public int TesteId { get; set; }
        public int CandidaturaId { get; set; }
        public virtual tb_Candidatura Candidatura { get; set; }
        public int DisciplinaId { get; set; }
        public virtual tb_Disciplina Disciplina { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Dt_Realizacao { get; set; }
        public bool Estado { get; set; }
        [Required]
        public int Duracacao { get; set; }
        public int Estado_Execusao { get; set; }
        public ICollection<tb_Teste_Resultado> TesteResultados { get; set; }
        public int? AgendamentoTesteId { get; set; }
        public virtual tb_Agendamento_Teste AgendamentoTeste { get; set; }
      
    }
}
