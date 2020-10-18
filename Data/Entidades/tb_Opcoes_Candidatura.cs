using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Data.Entidades
{
    public class tb_Opcoes_Candidatura
    {
        [Key]
        public int OpcaoId { get; set; }
        public int CursoId { get; set; }
        public virtual tb_Curso Curso { get; set; }
        public string Opcao { get; set; }
        public int CandidaturaId { get; set; }
        public virtual tb_Candidatura Candidatura { get; set; }

        public bool Estado { get; set; }


    }
}
