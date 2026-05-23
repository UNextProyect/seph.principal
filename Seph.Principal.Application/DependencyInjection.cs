using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Seph.Principal.Application.Common.Behaviors;
using System.Reflection;

namespace Seph.Principal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
            
        }
    }
}
