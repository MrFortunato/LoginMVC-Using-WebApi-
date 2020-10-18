using Exame_Online.Data.Entidades;

namespace Exame_Online.ModelsView
{
    public class CursoViewModel
    {
        public int  Id_Curso { get; set; }
        public string Nome_Curso { get; set; }
        public int Id_Departamento { get; set; }
        public string Departamento { get; set; }
        public int TotalDisciplinas { get; set; }
        public int Id_Disciplina { get; set; }
        public string Disciplina { get; set; }
        public int Perguntas { get; set; }
        public int Duracao { get; set; }
        public bool Estado { get; set; }
    }
}
