using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exame_Online.Data.Contexto;
using Exame_Online.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exame_Online.Controllers
{
    [Authorize]
    public class PerguntasController : Controller
    {
        private readonly Contexto db;
        public PerguntasController(Contexto _db)
        {
            db = _db;
        }
        [HttpGet]
        public List<PerguntaModelView> Lista()
        {
            var perguntas = db.Perguntas
                .Include(x => x.Disciplina)
                .Include(x => x.TipoPergunta).ToList();

            var lista = new List<PerguntaModelView>();

            foreach (var item in perguntas)
            {
                lista.Add(new PerguntaModelView
                {
                    Id_Pergunta = item.PerguntaId,
                    Pergunta = item.Descricao,
                    Disciplina = item.Disciplina.Nome_Disciplina,
                    TipoPergunta = item.TipoPergunta.Tipo_Pergunta,
                    Valor = item.Valor,
                    OpcaoRespostas = db.Respostas.Where(x => x.PerguntaId == item.PerguntaId).Count(),
                });
            }
            return lista;
        }
    }
}
