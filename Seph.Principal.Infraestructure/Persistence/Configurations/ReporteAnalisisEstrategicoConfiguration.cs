using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad
    /// ReporteAnalisisEstrategico para Entity Framework Core.
    /// </summary>
    public sealed class ReporteAnalisisEstrategicoConfiguration
        : IEntityTypeConfiguration<ReporteAnalisisEstrategico>
    {
        /// <summary>
        /// Configura la tabla, sus propiedades
        /// obligatorias y el índice único.
        /// </summary>
        public void Configure(
            EntityTypeBuilder<ReporteAnalisisEstrategico> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("ReporteAnalisisEstrategico");

            // Llave primaria de la entidad.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdMapInstitucionPeriodo)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            /*
             * Una institución solamente puede tener
             * un análisis estratégico por periodo.
             */
            builder.HasIndex(x => x.IdMapInstitucionPeriodo)
                .IsUnique();
        }
    }
}
