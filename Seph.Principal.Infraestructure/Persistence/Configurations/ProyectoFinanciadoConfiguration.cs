using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad
    /// ProyectoFinanciado
    /// para Entity Framework Core.
    /// </summary>
    public sealed class ProyectoFinanciadoConfiguration
        : IEntityTypeConfiguration<ProyectoFinanciado>
    {
        /// <summary>
        /// Configura la estructura de la tabla
        /// y sus propiedades obligatorias.
        /// </summary>
        public void Configure(
            EntityTypeBuilder<ProyectoFinanciado> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("ProyectoFinanciado");

            // Llave primaria.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdReporteFinanza)
                .IsRequired();

            builder.Property(x => x.StrNombre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.StrOrigenFinanciamiento)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.StrObjetivo)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}