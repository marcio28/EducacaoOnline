using EducacaoOnline.GestaoDeConteudo.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.GestaoDeConteudo.Data.Context
{
    public class GestaoConteudoContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }

        public GestaoConteudoContext() { }

        public GestaoConteudoContext(DbContextOptions<GestaoConteudoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CursoConfiguration());
        }
    }
}