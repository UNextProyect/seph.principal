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
    public sealed class MapInstitucionPeriodoConfiguration : IEntityTypeConfiguration<MapInstitucionPeriodo>
    {
        public void Configure(EntityTypeBuilder<MapInstitucionPeriodo> builder)
        {
            builder.ToTable("MapInstitucionPeriodo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdInstitucion)
                .IsRequired();

            builder.Property(x => x.IdPeriodo)
                .IsRequired();

            builder.Property(x => x.BitCapturaAbierta)
                .IsRequired();

            builder.Property(x => x.DateFechaApertura);

            builder.Property(x => x.DateFechaCierre);

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            builder.HasIndex(x => new { x.IdInstitucion, x.IdPeriodo })
                .IsUnique();

            builder.HasOne(x => x.Institucion)
                .WithMany()
                .HasForeignKey(x => x.IdInstitucion)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Periodo)
                .WithMany()
                .HasForeignKey(x => x.IdPeriodo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
