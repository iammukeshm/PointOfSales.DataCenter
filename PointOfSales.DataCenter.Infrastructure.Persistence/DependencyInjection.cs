using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using PointOfSales.DataCenter.Infrastructure.Persistence.Helpers;
using PointOfSales.DataCenter.Infrastructure.Persistence.Models;
using PointOfSales.DataCenter.Infrastructure.Persistence.Repositories;
using PointOfSales.DataCenter.Infrastructure.Persistence.Services;
using PointOfSales.DataCenter.Infrastructure.Persistence.Services.Email;

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
            services.AddTransient<IEmailService, EmailService>();

            

            //Repositories
            //Generic
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //Specific Repositories
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IPersonRepositoryAsync, PersonRepositoryAsync>();
            services.AddTransient<IInvoiceRepositoryAsync, InvoiceRepositoryAsync>();
            services.AddTransient<IInvoiceDetailRepositoryAsync, InvoiceDetailRepositoryAsync>();
            services.AddAuthentication();

            //Keep this always at last. JWT
            AuthenticationHelper.ConfigureService(services, configuration["JwtSecurityToken:Issuer"], configuration["JwtSecurityToken:Audience"], configuration["JwtSecurityToken:Key"]);


            return services;
        }
    }
}
