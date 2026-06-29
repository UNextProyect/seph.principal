using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class InstitucionConfiguration : IEntityTypeConfiguration<Institucion>
    {
        public void Configure(EntityTypeBuilder<Institucion> builder)
        {
            builder.ToTable("Institucion");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.StrNombre).HasMaxLength(250).IsRequired();
            builder.Property(x => x.IdMunicipio).IsRequired();
            builder.Property(x => x.DateTimeFechaRegistro).IsRequired();
            builder.Property(x => x.BitActivo).IsRequired();
        }
    }
}
