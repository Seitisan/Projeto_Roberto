using System;
using System.Threading.Tasks;
using API.Data.IRepository;
using API.DTOS;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("pesquisa")]
    [ApiController]
    public class PesquisasController : ControllerBase
    {
        private readonly IPesquisaRepository _repo;

        private readonly IMapper _mapper;

        public PesquisasController(IPesquisaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> RegisterCadastroPesquisa(CadastroPesquisaToRegister cadastroPesquisa){

            int codigoCadastro = int.MinValue;
            try
            {
                codigoCadastro = await _repo.RegisterCadastrarPesquisa(_mapper.Map<CadastroPesquisa>(cadastroPesquisa));
            }catch
            {
                return BadRequest();
            }
            return Ok(codigoCadastro);
        }

        [HttpPost]
        public IActionResult RegisterPesquisa(PesquisaToRegister pesquisa){
            
            try
            {
                _repo.RegisterPesquisa(_mapper.Map<Pesquisa>(pesquisa));
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("cadastro")]
        public async Task<IActionResult> GetCadastroPesquisa(int id, string turma){

            var cadastroPesquisa = await _repo.GetCadastrarPesquisa(id, turma);

            if(cadastroPesquisa==null){
                return BadRequest();
            }

            var cadastroPesquisaToReturn = _mapper.Map<CadastroPesquisaToReturn>(cadastroPesquisa);

            return Ok(cadastroPesquisaToReturn);
        }
    }
}