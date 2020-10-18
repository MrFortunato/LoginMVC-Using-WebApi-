using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Exame_Online.Data.Entidades.IdentyModel;

namespace Exame_Online.Data.Entidades
{
    public class tb_DadosPessoais
    {
        [Key]
        public int DadosPessoaisId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }

        [MaxLength(50)]
        [Required]
        public string SobreNome { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Digite a data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
     
        public DateTime Dt_Nasc { get; set; }

        [MaxLength(10), MinLength(1)]
        [Required]
        public string Genero { get; set; }
        [Required]
        public int? EstadoCivil { get; set; }

        [MaxLength(20)]
        [Required]
        public string Nr_BI { get; set; }

        [MaxLength(15)]
        [Required]
        public string Telefone { get; set; }

        [MaxLength(255)]
        public string UrlFoto { get; set; }

        public virtual Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public virtual ICollection<tb_Candidatura> Candidaturas { get; set; }


    }
}
