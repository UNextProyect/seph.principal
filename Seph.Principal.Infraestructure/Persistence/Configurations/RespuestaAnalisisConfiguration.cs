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
    /// RespuestaAnalisis para Entity Framework Core.
    /// </summary>
    public sealed class RespuestaAnalisisConfiguration
        : IEntityTypeConfiguration<RespuestaAnalisis>
    {
        /// <summary>
        /// Configura la tabla, sus propiedades,
        /// relaciones e índice único.
        /// </summary>
        public void Configure(
            EntityTypeBuilder<RespuestaAnalisis> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("RespuestaAnalisis");

            // Llave primaria.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdAnalisisEstrategico)
                .IsRequired();

            builder.Property(x => x.IdPreguntaAnalisis)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            /*
             * La respuesta puede permanecer vacía
             * porque las preguntas no son obligatorias.
             */
            builder.Property(x => x.StrRespuesta)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            /*
             * Conserva el texto histórico de la pregunta
             * tal como estaba al registrar la respuesta.
             */
            builder.Property(x => x.StrPregunta)
                .HasMaxLength(300)
                .IsRequired();

            /*
             * Relación entre la respuesta
             * y el reporte de análisis estratégico.
             */
            builder.HasOne(x => x.AnalisisEstrategico)
                .WithMany(x => x.RespuestasAnalisis)
                .HasForeignKey(x => x.IdAnalisisEstrategico)
                .OnDelete(DeleteBehavior.Restrict);

            /*
             * Relación entre la respuesta
             * y la pregunta original del catálogo.
             */
            builder.HasOne(x => x.PreguntaAnalisis)
                .WithMany()
                .HasForeignKey(x => x.IdPreguntaAnalisis)
                .OnDelete(DeleteBehavior.Restrict);

            /*
             * Evita registrar más de una respuesta
             * para la misma pregunta dentro del análisis.
             */
            builder.HasIndex(x => new
            {
                x.IdAnalisisEstrategico,
                x.IdPreguntaAnalisis
            })
            .IsUnique();
        }
    }
}
