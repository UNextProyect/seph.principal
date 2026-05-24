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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }

}
