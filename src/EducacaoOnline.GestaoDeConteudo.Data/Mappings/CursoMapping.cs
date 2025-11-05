
using EducacaoOnline.GestaoDeConteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.GestaoDeConteudo.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired();
        }
    }
}