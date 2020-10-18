using System;
using System.Collections.Generic;
using System.Linq;

namespace Exame_Online.ModelsView
{
    public class RespostaModel
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
        public List<PertuntaXModel> Opcoes { get; set; }
        public List<EscolhaModel> EscolhaUser { get; set; }
        public List<int> UserEscolhaId
        {
            get
            {
                return EscolhaUser == null ? new List<int>() :
                    EscolhaUser.Where(x => x.Selecionado == "on" || "true"
                    .Equals(x.Selecionado, StringComparison.InvariantCultureIgnoreCase))
                    .Select(x => x.Id_Escolha).ToList();
            }
        }
        public class EscolhaModel
        {
            public int Id_Escolha { get; set; }
            public string Selecionado { get; set; }


        }

        public class PertuntaXModel
        {
            public int Id_Opcao { get; set; }
            public string Label { get; set; }
            public string Respostax { get; set; }
            public string RespostaTexte { get; set; }
            public string Selecionado { get; set; }
            public string TipoPergunta { get; set; }
            public decimal cotacao { get; set; }
        }
    }
}
