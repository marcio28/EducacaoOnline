using EducacaoOnline.Core.Data;
using EducacaoOnline.Core.Messages;
using EducacaoOnline.GestaoConteudo.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.GestaoConteudo.Data.Context
{
    public class GestaoConteudoContext : DbContext, IUnitOfWork
    {
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        public GestaoConteudoContext() { }

        public GestaoConteudoContext(DbContextOptions<GestaoConteudoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoConteudoContext).Assembly);

            foreach (var tipoEntidade in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())) tipoEntidade.DeleteBehavior = DeleteBehavior.ClientCascade;

            modelBuilder.Ignore<Event>();

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}