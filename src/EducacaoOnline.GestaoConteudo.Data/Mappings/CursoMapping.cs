
using EducacaoOnline.GestaoConteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.GestaoConteudo.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired();

            builder.OwnsOne(c => c.ConteudoProgramatico, cp =>
            {
                cp.Property(c => c.Descricao)
                  .IsRequired()
                  .HasColumnName("ConteudoProgramatico_Descricao");
            });
        }
    }
}