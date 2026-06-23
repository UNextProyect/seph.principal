using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Infraestructure.Identity;

namespace Seph.Principal.Infraestructure.Persistence
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IApplicationDbContext, IUnitOfWork
    {
        public DbSet<RefreshTokenSession> RefreshTokenSessions => Set<RefreshTokenSession>();
        public DbSet<Institucion> Instituciones => Set<Institucion>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<CatSexo> CatSexo => Set<CatSexo>();
        public DbSet<EmailVerificationCode> EmailVerificationCodes => Set<EmailVerificationCode>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            builder.Entity<ApplicationRole>(b =>
            {
                b.Ignore("CreatedBy");
                b.Ignore("UpdatedBy");
                b.Ignore("CreatedAtUtc");
                b.Ignore("UpdatedAtUtc");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
}