namespace Exame_Online.ModelsView
{
    public class PerguntaModelView
    {
        public int Id_Pergunta { get; set; }
        public string Pergunta { get; set; }
        public int OpcaoRespostas { get; set; }
        public int IdTeste { get; set; }
        public int NrPergunta { get; set; }
        public int OrderPerguntas { get; set; }
        public string TipoPergunta { get; set; }
        public string Disciplina { get; set; }
        public decimal Valor { get; set; }  
        public int Total { get; set; }
        public int TotalRespondidas { get; set; }


    }
}
