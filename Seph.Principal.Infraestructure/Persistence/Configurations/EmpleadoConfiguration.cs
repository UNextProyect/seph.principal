using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(
            EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleado");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.StrNombre)
               .HasMaxLength(250)
               .IsRequired();

            builder.Property(x => x.StrApellidoPat)
               .HasMaxLength(250)
               .IsRequired();

            builder.Property(x => x.StrApellidoMat)
               .HasMaxLength(250);

            builder.Property(x => x.StrCurp)
               .HasMaxLength(18)
               .IsRequired();

            builder.Property(x => x.IdSexo)
                .IsRequired();

            builder.Property(x => x.StrSNII)
                .HasMaxLength(12);

            builder.Property(x => x.StrRutaIne)
              .HasMaxLength(500);

            builder.Property(x => x.StrRutaFotografia)
                .HasMaxLength(500);

            builder.Property(x => x.BitDatosAcademicosCompletos)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaBaja)
                .IsRequired();
        }
    }
}
