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
    public sealed class HistorialContratoConfiguration : IEntityTypeConfiguration<HistorialContrato>
    {
        public void Configure(
            EntityTypeBuilder<HistorialContrato> builder)
        {
            builder.ToTable("HistorialContrato");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DateFechaIngreso)
                .IsRequired();

            builder.Property(x => x.DateFechaInicio)
                .IsRequired();

            builder.Property(x => x.IdEmpleado)
                .IsRequired();

            builder.Property(x => x.IdInstitucion)
                .IsRequired();

            builder.Property(x => x.IdTipoPersonal)
                .IsRequired();

            builder.Property(x => x.IdTipoContrato)
                .IsRequired();

            builder.Property(x => x.StrOtroTipoContrato)
                .HasMaxLength(100);

            builder.Property(x => x.IdArea)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaBaja)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();
        }
    }
}