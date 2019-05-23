namespace API.Models
{
    public class Resposta
    {
        public int Id { get; set; }

        public int IdPesquisa { get; set; }

        public int IdPergunta { get; set; }

        public int IdDocente { get; set; }

        public string DocenteMateria { get; set; }

        public string Coordenador { get; set; }

        public int ValorResposta { get; set; }

        public int ValorImportancia { get; set; }

        public Pesquisa Pesquisa { get; set; }
    }
}