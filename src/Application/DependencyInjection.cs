using Application.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped<IRelativeStrengthService, RelativeStrengthService>();
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
