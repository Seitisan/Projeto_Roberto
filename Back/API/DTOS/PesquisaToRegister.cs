using System.Collections.Generic;
using API.Models;

namespace API.DTOS
{
    public class PesquisaToRegister
    {
        public int IdCadastroPesquisa { get; set; }

        public string NomeAluno { get; set; }

        public string Comentario { get; set; }

        public ICollection<Resposta> Respostas { get; set; }
       
    }
}