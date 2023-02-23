using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smart.RentService.Infrastructure.Data;
using Smart.RentService.Infrastructure.Data.Interceptors;
using Smart.RentService.Infrastructure.Interfaces;
using Smart.RentService.Infrastructure.Services;
using Smart.RentService.SharedKernel.Interfaces;

namespace Smart.RentService.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddServices()
                .AddInterceptors()
                .AddAppDbContext(configuration)
                .AddRepositories();
        }

        public static async Task<IServiceProvider> SeedData(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
                await initialiser.InitialiseAsync();
                await initialiser.SeedAsync();
            }

            return serviceProvider;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<AppDbContextInitialiser>();
            return services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }

        private static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });
        }

        private static IServiceCollection AddInterceptors(this IServiceCollection services)
        {
            return services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
                           .AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        }
    }
}
