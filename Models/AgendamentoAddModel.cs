using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exame_Online.Models
{
    public class AgendamentoAddModel
    {
        public int Id_Agendamento { get; set; }
        public int Id_Departamento { get; set; }
        public int Id_Curso { get; set; }
        public string Dt_Realizacao { get; set; }
        public string Dt_expirar { get; set; }
        public string Horario { get; set; }
        public int Id_Disciplina { get; set; }

        public int Id_SalaExame { get; set; }
        public int Estado { get; set; }
    }
}
