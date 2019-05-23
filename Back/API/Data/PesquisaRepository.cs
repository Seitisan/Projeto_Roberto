using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.IRepository;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PesquisaRepository : IPesquisaRepository
    {
        private readonly DataContext _context;

        public PesquisaRepository (DataContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterCadastrarPesquisa(CadastroPesquisa cadastroPesquisa)
        {
            cadastroPesquisa.Turma = cadastroPesquisa.Turma.ToLower();
            cadastroPesquisa.Coordenador = cadastroPesquisa.Coordenador.ToLower();
            cadastroPesquisa.Curso = cadastroPesquisa.Curso.ToLower();
            
            foreach(var item in cadastroPesquisa.Docentes){
                item.NomeDocente = item.NomeDocente.ToLower();
                item.Materia = item.Materia.ToLower();
            }
            cadastroPesquisa.PesquisaCriada = DateTime.Now.Date;

            await _context.CadastroPesquisas.AddAsync(cadastroPesquisa);
            await _context.SaveChangesAsync();

            return cadastroPesquisa.Id;
        }

        public void RegisterPesquisa(Pesquisa pesquisa)
        {
            if(pesquisa.NomeAluno != null){
                pesquisa.NomeAluno = pesquisa.NomeAluno.ToLower();
            }

            pesquisa.Respondido = DateTime.Now.Date;

            _context.Pesquisas.Add(pesquisa);

            _context.SaveChangesAsync();
        }

        public async Task<CadastroPesquisa> GetCadastrarPesquisa(int id ,string turma)
        {
            var cadastroPesquisa = await _context.CadastroPesquisas.Include(d => d.Docentes).FirstOrDefaultAsync(it => it.Turma == turma && it.Id == id);

            if(cadastroPesquisa == null){
                return null;
            }

            return cadastroPesquisa;
        }
    }
}