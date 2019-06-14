using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            CadastroPesquisa cadastroPesquisa = await _context.CadastroPesquisas.Include(d => d.Docentes).FirstOrDefaultAsync(it => it.Turma == turma.ToLower() && it.Id == id);

            if(cadastroPesquisa == null){
                return null;
            }

            return cadastroPesquisa;
        }

        public async Task<CadastroPesquisa> GetCadastrarPesquisa(int id)
        {
            CadastroPesquisa cadastroPesquisa = await _context.CadastroPesquisas.Include(d => d.Docentes).FirstOrDefaultAsync(it => it.Id == id);

            if (cadastroPesquisa == null)
            {
                return null;
            }

            return cadastroPesquisa;
        }

        public async Task<List<Pesquisa>> GetPesquisas(int id){
            List<Pesquisa> listaPesquisa = await _context.Pesquisas.Include(r => r.Respostas).Where(it => it.IdCadastroPesquisa == id).ToListAsync();

            if(listaPesquisa == null){
                return new List<Pesquisa>();
            }

            return listaPesquisa;
        }

        public byte[] GeraCSV(CadastroPesquisa cadastro, List<Pesquisa> pesquisas){

            StringBuilder sb = new StringBuilder().Clear();

            double qtdAlunos = cadastro.QuantidadeAluno;

            double qtdPesq = pesquisas.Count;

            DateTime hoje = DateTime.Now;

            sb.Append(Environment.NewLine).Append(Environment.NewLine).Append(Environment.NewLine);
            sb.Append(",Avaliacao de Satisfacao - Participante (CAI - CT - FIC - CST - POS)").Append(Environment.NewLine);
            sb.Append(",Total alunos turma,," + cadastro.QuantidadeAluno + ",100%").Append(Environment.NewLine);
            sb.Append(",Total alunos responderam,," + qtdPesq + "," + ((qtdPesq / qtdAlunos) * 100).ToString() + "%").Append(Environment.NewLine).Append(Environment.NewLine);
            sb.Append(",Curso,,,,,Turma,Periodo").Append(Environment.NewLine);
            sb.Append("," + cadastro.Curso + ",,,,," + cadastro.Turma).Append(Environment.NewLine).Append(Environment.NewLine);
            sb.Append(",Data," + hoje.Day + "/" + hoje.Month + "/" + hoje.Year).Append(Environment.NewLine).Append(Environment.NewLine).Append(Environment.NewLine);
            sb.Append(",,,,ITENS DE AVALIACAO,,,,Nivel de Satisfacao,,,,N/A,,Nivel de Satisfacao,,,,N/A").Append(Environment.NewLine);
            sb.Append(",,,,,,,,RUIM(1),REGULAR(2),BOM(3),OTIMO(4),,,RUIM,REG.,BOM,OTI.,").Append(Environment.NewLine);
            PreenchePerguntasRespostas(ref sb, pesquisas, cadastro);
            sb.Append("Comentarios (reclamacoes e sugestoes): (Se necessario utilize o verso):").Append(Environment.NewLine);
            foreach (Pesquisa pesquisa in pesquisas)
            {
                if (pesquisa.Comentario != null)
                {
                    if (!pesquisa.Comentario.Equals(string.Empty))
                    {
                        sb.Append(pesquisa.Comentario).Append(Environment.NewLine);
                    }
                }
            }
            return new UTF8Encoding(true).GetBytes(sb.ToString());
        }

        private void PreenchePerguntasRespostas(ref StringBuilder sb, List<Pesquisa> pesquisas, CadastroPesquisa cadastro)
        {
            int[] qtdRuinsPerguntas = QuantidadeRespostas(4, pesquisas);
            int[] qtdRegularPerguntas = QuantidadeRespostas(3, pesquisas);
            int[] qtdBomPerguntas = QuantidadeRespostas(2, pesquisas);
            int[] qtdOtimasPerguntas = QuantidadeRespostas(1, pesquisas);
            int i;
            int aux = 9;

            string[] perguntas = {
                "1. Limpeza e conservacao da sala de aula e da oficina",
                "2. Disponibilidade de equipamentos (maquinas e ferramentas) para realizacao do curso",
                "3. Qual. de (apostilas livros e textos) quanto a impressao e adequacao da informacao",
                "4. O aprendizado (na teoria e na pratica) em relacao ao esperado",
                "5. O conteudo do curso em relacao as expectativas",
                "6. Atendimento na recepcao / secretaria da Escola",
                "7. Atendimento telefonico da Escola",
                "8. Atendimento na cantina / lanchonete",
                "9. Atendimento da Biblioteca",
                "10. Cumprimento do horario de aulas",
                "11. Cumprimento dos objetivos propostos para o curso",
                "12. Preocupacao com o aproveitamento dos alunos",
                "13. Dominio sobre os assuntos tratados",
                "14. Abertura para comunicacao",
                "15. Atencao dispensada quando procurado",
                "16. Cordialidade e respeito com alunos",
                "17. Efetividade na solucao e orientacao quando solicitado"
             };

            for (i = 0; i < 9; i++)
            {
                sb.Append(perguntas[i] + ",,,,,,,," + qtdRuinsPerguntas[i] + "," + qtdRegularPerguntas[i] + ","
                + qtdBomPerguntas[i] + "," + qtdOtimasPerguntas[i] + ",,," + Porcentagem(qtdRuinsPerguntas[i], pesquisas.Count) + "%," +
                Porcentagem(qtdRegularPerguntas[i], pesquisas.Count) + "%," + Porcentagem(qtdBomPerguntas[i], pesquisas.Count) + "%," +
                Porcentagem(qtdOtimasPerguntas[i], pesquisas.Count) + "%").Append(Environment.NewLine);
            }

            sb.Append(Environment.NewLine);
            for (i = 9; i < 13; i++)
            {
                PreenchePerguntasDocentes(ref sb, pesquisas, cadastro, qtdRuinsPerguntas, qtdRegularPerguntas, qtdBomPerguntas, qtdOtimasPerguntas, ref i, ref aux, perguntas);
            }
            sb.Append(",,,Coordenacao (nome):").Append(Environment.NewLine);
            PreenchePerguntasCoordenacao(ref sb, pesquisas, qtdRuinsPerguntas, qtdRegularPerguntas, qtdBomPerguntas, qtdOtimasPerguntas, ref i, ref aux, perguntas);
            sb.Append(Environment.NewLine);
        }

        private void PreenchePerguntasCoordenacao(ref StringBuilder sb, List<Pesquisa> pesquisas, int[] qtdRuinsPerguntas, 
        int[] qtdRegularPerguntas, int[] qtdBomPerguntas, int[] qtdOtimasPerguntas, ref int i, ref int aux, string[] perguntas)
        {
            for (i = 13; i < 17;i++){
                sb.Append(",,," + perguntas[i] + ",,,,," + qtdRuinsPerguntas[aux] + "," + qtdRegularPerguntas[aux] + ","
                                + qtdBomPerguntas[aux] + "," + qtdOtimasPerguntas[aux] + ",,," + Porcentagem(qtdRuinsPerguntas[aux], pesquisas.Count).ToString() + "%," +
                                Porcentagem(qtdRegularPerguntas[aux], pesquisas.Count).ToString() + "%," + Porcentagem(qtdBomPerguntas[aux], pesquisas.Count).ToString() + "%," +
                                Porcentagem(qtdOtimasPerguntas[aux], pesquisas.Count).ToString() + "%").Append(Environment.NewLine);
                aux++;
            }
        }

        private void PreenchePerguntasDocentes(ref StringBuilder sb, List<Pesquisa> pesquisas, CadastroPesquisa cadastro, 
        int[] qtdRuinsPerguntas, int[] qtdRegularPerguntas, int[] qtdBomPerguntas, 
        int[] qtdOtimasPerguntas, ref int i, ref int aux, string[] perguntas)
        {
            sb.Append("Docentes (Nomes):,,,," + perguntas[i]).Append(Environment.NewLine);
            for (int j = 1; j < 8; j++)
            {
                sb.Append(","+j.ToString() + ". " + cadastro.Docentes.ToList()[j - 1].NomeDocente + ",,,,,,," + qtdRuinsPerguntas[aux] + "," + qtdRegularPerguntas[aux] + ","
                + qtdBomPerguntas[aux] + "," + qtdOtimasPerguntas[aux] + ",,," + Porcentagem(qtdRuinsPerguntas[aux], pesquisas.Count).ToString() + "%," +
                Porcentagem(qtdRegularPerguntas[aux], pesquisas.Count).ToString() + "%," + Porcentagem(qtdBomPerguntas[aux], pesquisas.Count).ToString() + "%," +
                Porcentagem(qtdOtimasPerguntas[aux], pesquisas.Count).ToString() + "%").Append(Environment.NewLine);
                if(j==7){
                    sb.Append(Environment.NewLine);
                }
                aux++;
            }
        }

        private int[] QuantidadeRespostas(int v, List<Pesquisa> pesquisas)
        {
            int[] resultados = new int[41];
            int index = 0;
            foreach(var item in resultados){
                int soma = 0;
                foreach(Pesquisa pesquisa in pesquisas){
                    if(pesquisa.Respostas.ToList()[index].ValorResposta == v)
                    {
                        soma++;
                    }
                }
                resultados[index] = soma;
                index++;
            }

            return resultados;
        }

        private double Porcentagem(double v, double count)
        {
            double porcentagem = 0.0;
            if(v==0){
                return 0;
            }
            porcentagem = (v / count)*100;
            return Math.Round(porcentagem, 2);
        }
    }
}