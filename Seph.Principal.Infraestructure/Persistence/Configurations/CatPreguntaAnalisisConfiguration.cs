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
    /*
     * Configura el mapeo de la entidad
     * CatPreguntaAnalisis en la base de datos.
     */
    public sealed class CatPreguntaAnalisisConfiguration
        : IEntityTypeConfiguration<CatPreguntaAnalisis>
    {
        public void Configure(
            EntityTypeBuilder<CatPreguntaAnalisis> builder)
        {
            builder.ToTable("CatPreguntaAnalisis");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.StrPregunta)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            builder.Property(x => x.IntOrden)
                .IsRequired();

            /*
             * Evita registrar dos preguntas
             * con exactamente el mismo texto.
             */
            builder.HasIndex(x => x.StrPregunta)
                .IsUnique();

            /*
             * Facilita la consulta de las preguntas
             * activas en el orden correspondiente.
             */
            builder.HasIndex(x => new
            {
                x.BitActivo,
                x.IntOrden
            });
        }
    }
}
