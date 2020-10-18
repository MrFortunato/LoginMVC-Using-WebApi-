using System.Collections.Generic;

namespace Exame_Online.ModelsView
{


    public class CandidatoViewModel
    {
        public int Id { get; set; }
        public string Nome_Completo { get; set; }
        public string Telf { get; set; }
        public string Foto { get; set; }
        public string Endereco { get; set; }
        public string Curso { get; set; }
        public string Dt_Nasc { get; set; }
        public int Id_Teste { get; set; }
        public string Dt_Agendamento { get; set; }
        public bool Estado { get; set; }
        public string Disciplina { get; set; }
        public string Selecionado { get; set; }

        public List<CandidaturaViewModel> Candidaturas { get; set; }
    }

    public class CandidaturaViewModel
    {
        public int Id_Candidato { get; set; }
        public string Curso { get; set; }
        public string Opcao2 { get; set; }
        public string Ramo { get; set; }
        public bool Estado { get; set; }
  
  
    }

    public class testeInfo
    {
        public string Disciplina { get; set; }
        public string Dt_Exp { get; set; }
        public int Tempo { get; set; }
        public string Estado { get; set; }
        public int TotalPeruntas { get; set; }
        public int Id_Teste { get; set; }
        public int NotaFinal { get; set; }
        public decimal Nota_U { get; set; }
        public decimal Nota_M { get; set; }
        public decimal Nota_T { get; set; }
        public string DataAgendamento { get; set; }

    }
    
}
