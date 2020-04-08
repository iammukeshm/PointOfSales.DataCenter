using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSales.DataDistribution.Application.Interfaces;
using PointOfSales.DataDistribution.Infrastructure.Persistence.Context;
using PointOfSales.DataDistribution.Infrastructure.Persistence.Services;

namespace PointOfSales.DataDistribution.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            
            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddAuthentication();

            return services;
        }
    }
}
