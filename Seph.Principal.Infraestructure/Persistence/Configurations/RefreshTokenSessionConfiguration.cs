using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class RefreshTokenSessionConfiguration: IEntityTypeConfiguration<RefreshTokenSession>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenSession> builder) 
        {
            builder.ToTable("RefreshTokenSessions", "security");
            builder.HasKey(session => session.Id);

            builder.Property(session => session.TokenHash).HasMaxLength(128).IsRequired();
            builder.Property(session => session.DeviceId).HasMaxLength(128).IsRequired();
            builder.Property(session => session.IpAddress).HasMaxLength(64).IsRequired();
            builder.Property(session => session.Status).HasConversion<string>().HasMaxLength(32).IsRequired();

            builder.HasIndex(session => session.TokenHash).IsUnique();
            builder.HasIndex(session => new { session.UserId, session.DeviceId, session.Status });
        }
    }
}
