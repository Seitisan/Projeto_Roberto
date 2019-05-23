using System.Threading.Tasks;
using API.Models;

namespace API.Data.IRepository
{
    public interface IPesquisaRepository
    {
        Task<int> RegisterCadastrarPesquisa(CadastroPesquisa cadastroPesquisa);

        void RegisterPesquisa(Pesquisa pesquisa);

        Task<CadastroPesquisa> GetCadastrarPesquisa(int id, string turma);
    }
}