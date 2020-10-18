using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exame_Online.ModelsView
{
    public class AgendamentoModelView
    {
        public int Id_Agendamento { get; set; }
        public string Data_Real { get; set; }
        public string Data_Exp { get; set; }
        public string  Curso { get; set; }
        public string Disciplina { get; set; }
        public string Hora { get; set; }
        public bool Estado { get; set; }
        public int Vagas { get; set; }
    }
}
