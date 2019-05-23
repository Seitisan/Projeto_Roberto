using API.DTOS;
using API.Models;
using AutoMapper;
using System.Linq;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CadastroPesquisaToRegister, CadastroPesquisa>();
            CreateMap<CadastroPesquisa, CadastroPesquisaToReturn>();
            CreateMap<PesquisaToRegister, Pesquisa>();
        }
    }
}