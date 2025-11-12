using EducacaoOnline.GestaoAlunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.GestaoAlunos.Data.Mappings
{
    public class HistoricoAprendizadoMapping : IEntityTypeConfiguration<HistoricoAprendizado>
    {
        public void Configure(EntityTypeBuilder<HistoricoAprendizado> builder)
        {
            builder.ToTable("HistoricoAprendizado");

            builder.HasKey(c => c.Id);
        }
    }
}