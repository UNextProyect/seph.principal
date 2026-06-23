using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Domain.Repositories;
using Seph.Principal.Infraestructure.Authentication;
using Seph.Principal.Infraestructure.Authorization;
using Seph.Principal.Infraestructure.Email;
using Seph.Principal.Infraestructure.Identity;
using Seph.Principal.Infraestructure.Persistence;
using Seph.Principal.Infraestructure.Persistence.Repositories;
using System.Text;

namespace Seph.Principal.Infraestructure.DependencyInjection
{
    /*esta clase es la encargada de registrar todos los servicios relacionados con la infraestructura, 
     * como el contexto de la base de datos, los servicios de identidad, los servicios de autenticación y autorización,
     * etc. Esto permite mantener una separación clara entre la capa de 
     * infraestructura y las demás capas de la aplicación, facilitando el mantenimiento y la escalabilidad del código.*/
    public static class InfrastructureDependencyInjection
    {

        /*El método AddInfraestructure es una extensión de IServiceCollection que se encarga de registrar todos
         * los servicios relacionados con la infraestructura en el contenedor de dependencias de ASP.NET Core.*/
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ??
                throw new InvalidOperationException("La configuración  JWT es obligatoria");

            services.AddDbContext<ApplicationDbContext>(options => options.
                UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            /*El método AddIdentity se utiliza para configurar los servicios de identidad en la aplicación. 
             * Aquí se establecen las políticas de contraseña, bloqueo de cuenta y requisitos de usuario, 
             * así como la configuración para usar Entity Framework Core como el almacén de datos para la identidad.*/
            services.AddIdentityCore<ApplicationUser>(options =>
            {

                options.Password.RequiredLength = 12;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.User.RequireUniqueEmail = true;

            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager();
            /*El método AddAuthentication se utiliza para configurar los servicios de autenticación en la aplicación.
             */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.RequireHttpsMetadata = true;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"JWT Auth failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            Console.WriteLine($"JWT Challenge: {context.Error} - {context.ErrorDescription}");
                            return Task.CompletedTask;
                        }
                    };
                });

            /*El método AddAuthorizationBuilder se utiliza para configurar los servicios de autorización en la aplicación.
             */
            services.AddAuthorizationBuilder()
                .AddPolicy("Security.Admin", policy => policy.Requirements.Add(new PermissionRequirement("security.admin")))
                .AddPolicy("Users.Read", policy => policy.Requirements.Add(new PermissionRequirement("users.read")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IRefreshTokenSessionRepository, RefreshTokenSessionRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IInstitucionRepository, InstitucionRepository>();
            services.AddScoped<IEmpleadosRepository, EmpleadosRepository>();
            services.AddScoped<ICatSexoRepository, CatSexoRepository>();

            services.Configure<GoogleOptions>(configuration.GetSection(GoogleOptions.SectionName));
            services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();
            services.AddScoped<IEmailService, SmtpEmailService>();
            services.AddScoped<IEmailVerificationCodeRepository, EmailVerificationCodeRepository>();

            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            return services;
        }
    }
}
