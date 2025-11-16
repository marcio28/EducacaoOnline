using EducacaoOnline.GestaoConteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.GestaoConteudo.Data.Mappings
{
    public class AdministradorMapping : IEntityTypeConfiguration<Administrador>
    {
        public void Configure(EntityTypeBuilder<Administrador> builder)
        {
            builder.ToTable("Administrador");

            builder.HasKey(c => c.Id);
        }
    }
}