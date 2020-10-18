using System.Collections.Generic;

namespace Exame_Online.ModelsView
{
    public class PerguntaView
        {
            public int Id_Teste { get; set; }
            public int TestePerguntaId { get; set; }
            public int Id_Pergunta { get; set; }
            public int Pergunta_Nr { get; set; }
            public string Pergunta { get; set; }
            public string TipoPergunta { get; set; }
            public string Disciplina { get; set; }
            public int TotalPerguntas { get; set; }
            public decimal Valor { get; set; }
            public List<RespostaView> Opcoes { get; set; }



            public class RespostaView
            {
                public int Id_Opcao { get; set; }
                public string RespostaCandidato { get; set; }
                public string RespostaSistema { get; set; }
                public string RespostaTexte { get; set; }
                public string Selecionado { get; set; }
                public string TipoPergunta { get; set; }
                public decimal cotacao { get; set; }
            }
        }
    }

