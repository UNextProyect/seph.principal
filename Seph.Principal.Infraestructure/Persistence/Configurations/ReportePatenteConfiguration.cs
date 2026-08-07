using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad ReportePatente
    /// para Entity Framework Core.
    /// </summary>
    public sealed class ReportePatenteConfiguration
        : IEntityTypeConfiguration<ReportePatente>
    {
        /// <summary>
        /// Configura la estructura de la tabla, sus propiedades
        /// obligatorias y el índice único de la entidad.
        /// </summary>
        public void Configure(
            EntityTypeBuilder<ReportePatente> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("ReportePatente");

            // Llave primaria de la entidad.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdMapInstitucionPeriodo)
                .IsRequired();

            builder.Property(x => x.StrNombreTitulo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.StrNumeroRegistroSolicitud)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.IdTipoPatente)
                .IsRequired();

            builder.Property(x => x.IdEstatusPatente)
                .IsRequired();

            builder.Property(x => x.DateFechaSolicitud)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.DateFechaConcesion)
                .HasColumnType("date");

            builder.Property(x => x.StrTitularPatente)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            // El número de registro o solicitud
            // no puede repetirse entre patentes.
            builder.HasIndex(x => x.StrNumeroRegistroSolicitud)
                .IsUnique();
        }
    }
}