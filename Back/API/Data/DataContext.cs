using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CadastroPesquisa> CadastroPesquisas { get; set; }

        public DbSet<Pesquisa> Pesquisas { get; set; }

        public DbSet<Docente> Docentes { get; set; }

        public DbSet<Pergunta> Perguntas { get; set; }

        public DbSet<Resposta> Resposta { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Pesquisa>()
                .HasOne(cp => cp.CadastroPesquisa)
                .WithMany(p => p.Pesquisas)
                .HasForeignKey(fk => fk.IdCadastroPesquisa);
            builder.Entity<Pesquisa>()
                .HasMany(r => r.Respostas)
                .WithOne(p => p.Pesquisa)
                .HasForeignKey(fk => fk.IdPesquisa);
        }
    }
}