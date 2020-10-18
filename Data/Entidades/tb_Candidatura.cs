using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exame_Online.Data.Entidades
{
    public class tb_Candidatura
    {   
        [Key]
        public int CandidaturaId { get; set; }

        [Required]
        public DateTime Dt_Registro { get; set; }
        public int DadosPessoaisId { get; set; }
        public virtual tb_DadosPessoais DadosPessoais { get; set; }
        public int Periodo { get; set; }
        public int AnoLectivo { get; set; }
        public bool Estado { get; set; }
        public virtual ICollection<tb_Teste> Teste { get; set; }
        public virtual ICollection<tb_Opcoes_Candidatura> OpcoesCandidaturas { get; set; }

    }
}
