
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSales.DataCenter.Application.Interfaces.Jobs;
using PointOfSales.DataCenter.Infrastructure.Hangfire.Jobs;

namespace PointOfSales.DataCenter.Infrastructure.Hangfire
{
    public static class DependencyInjection
    {
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
            //services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            //services.AddHangfireServer(options =>
            //options.ServerName = "API Hangfire Server"
            //);
            services.AddTransient<IEmailScheduler, EmailScheduler>();
        }
    }
}
