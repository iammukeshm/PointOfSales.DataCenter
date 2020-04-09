using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using PointOfSales.DataCenter.Infrastructure.Persistence.Models;
using PointOfSales.DataCenter.Infrastructure.Persistence.Services;

namespace PointOfSales.DataCenter.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        //Check
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            //UserManager Service
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddAuthentication();

            return services;
        }
    }
}
