using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddDbContext<MercVistaContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "MercVistaDb"));
            //services.AddDbContext<MercVistaContext>(options =>
            //options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(assembly.FullName)));

            services.AddScoped<IAcaoRepository, AcaoRepository>();

            TypeAdapterConfig.GlobalSettings.Scan(assembly);
            return services;
        }
    }
}
