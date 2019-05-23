using System.Collections.Generic;
using API.Models;

namespace API.DTOS
{
    public class CadastroPesquisaToReturn
    {
        public int Id { get; set; }
        public string Curso { get; set; }

        public string Turma { get; set; }

        public string Coordenador { get; set; }

        public ICollection<Docente> Docentes { get; set; }
    }
}