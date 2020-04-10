using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PointOfSales.Domain.Settings;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using System;
using System.IO;

namespace PointOfSales.BackgroundServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Directory.CreateDirectory(Path.Combine(environment.ContentRootPath, "logs"));
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("Hangfire", LogEventLevel.Information)
                .WriteTo.File(Path.Combine(environment.ContentRootPath, "logs", "Log-.txt"),
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    rollingInterval: RollingInterval.Day
                    )
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(Matching.FromSource("PointOfSales.DataCenter.Infrastructure.Persistence.Services.Email"))
                    .WriteTo.File(Path.Combine(environment.ContentRootPath, "logs", "EmailLogs-.txt"),
                        fileSizeLimitBytes: 10 * 1024 * 1024,
                        rollOnFileSizeLimit: true,
                        shared: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(1),
                        rollingInterval: RollingInterval.Day
                        ))
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddHangfire(x =>
            {
                var options = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                };
                x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), options);
            });
            services.AddHangfireServer(options =>
            options.ServerName = "DataCenter.BackgroundServer"
            );
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHangfireDashboard("");
            //app.UseHangfireServer();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
