using EducacaoOnline.Core.Data;
using EducacaoOnline.Core.Messages;
using EducacaoOnline.GestaoAlunos.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.GestaoAlunos.Data.Context
{
    public class GestaoAlunosContext : DbContext, IUnitOfWork
    {
        public DbSet<Aluno> Alunos { get; set; }

        public GestaoAlunosContext() { }

        public GestaoAlunosContext(DbContextOptions<GestaoAlunosContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoAlunosContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientCascade;

            modelBuilder.Ignore<Event>();

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}