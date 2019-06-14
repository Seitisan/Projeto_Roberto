using System.Threading.Tasks;
using System.Collections.Generic;
using API.Models;

namespace API.Data.IRepository
{
    public interface IPesquisaRepository
    {
        Task<int> RegisterCadastrarPesquisa(CadastroPesquisa cadastroPesquisa);
        void RegisterPesquisa(Pesquisa pesquisa);
        Task<CadastroPesquisa> GetCadastrarPesquisa(int id, string turma);
        Task<CadastroPesquisa> GetCadastrarPesquisa(int id);
        Task<List<Pesquisa>> GetPesquisas(int id);
        byte[] GeraCSV(CadastroPesquisa cadastro, List<Pesquisa> pesquisas);
    }
}