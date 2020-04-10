
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PointOfSales.DataCenter.Infrastructure.Hangfire
{
    public static class DependencyInjection
    {
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer(options =>
            options.ServerName = "API Hangfire Server"
            );
        }
    }
}
