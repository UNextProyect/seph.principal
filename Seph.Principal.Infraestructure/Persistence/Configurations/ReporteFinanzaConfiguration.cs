using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad ReporteFinanza
    /// para Entity Framework Core.
    /// </summary>
    public sealed class ReporteFinanzaConfiguration : IEntityTypeConfiguration<ReporteFinanza>
    {
        /// <summary>
        /// Configura la estructura de la tabla, sus propiedades
        /// obligatorias y el índice único de la entidad.
        /// </summary>
        public void Configure(EntityTypeBuilder<ReporteFinanza> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("ReporteFinanza");

            // Llave primaria de la entidad.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdMapInstitucionPeriodo)
                .IsRequired();

            builder.Property(x => x.MoneyPresupuestoAnual)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.MoneySubsidioEstatal)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.MoneySubsidioFederal)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.MoneyIngresosPropios)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.MoneyGastoEjercido)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.MoneyGastoAlumno)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.BitAdeudos)
                .IsRequired();

            builder.Property(x => x.MoneyMontoAdeudo)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            // Una institución únicamente puede tener
            // un reporte financiero por periodo.
            builder.HasIndex(x => x.IdMapInstitucionPeriodo)
                .IsUnique();
        }
    }
}