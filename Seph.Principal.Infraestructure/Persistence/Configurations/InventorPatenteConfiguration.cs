using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad
    /// InventorPatente
    /// para Entity Framework Core.
    /// </summary>
    public sealed class InventorPatenteConfiguration
        : IEntityTypeConfiguration<InventorPatente>
    {
        /// <summary>
        /// Configura la estructura de la tabla
        /// y sus propiedades obligatorias.
        /// </summary>
        public void Configure(
            EntityTypeBuilder<InventorPatente> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("InventorPatente");

            // Llave primaria.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdPatente)
                .IsRequired();

            builder.Property(x => x.StrNombreCompleto)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}