using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Pesquisa
    {
        public int Id { get; set; }

        public int IdCadastroPesquisa { get; set; }

        public string NomeAluno { get; set; }

        public ICollection<Resposta> Respostas { get; set; }

        public DateTime Respondido { get; set; }
        
        public string Comentario { get; set; }

        public CadastroPesquisa CadastroPesquisa { get; set; }
    }
}