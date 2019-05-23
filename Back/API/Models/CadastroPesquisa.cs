using System;
using System.Collections.Generic;

namespace API.Models
{
    public class CadastroPesquisa
    {
        public int Id { get; set; }

        public string TipoCurso { get; set; }

        public string Curso { get; set; }

        public string Turma { get; set; }

        public int QuantidadeAluno { get; set; }

        public string Coordenador { get; set; }

        public ICollection<Docente> Docentes { get; set; }

        public DateTime PesquisaCriada { get; set; }

        public ICollection<Pesquisa> Pesquisas { get; set; }
    }
}