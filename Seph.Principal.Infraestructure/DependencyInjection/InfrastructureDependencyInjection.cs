using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Infraestructure.Authentication;
using Seph.Principal.Infraestructure.Persistence;

namespace Seph.Principal.Infraestructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ??
                throw new InvalidOperationException("La configuración  JWT es obligatoria");

            services.AddDbContext<ApplicationDbContext>(options => options.
                UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
