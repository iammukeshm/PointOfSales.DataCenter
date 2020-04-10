using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PointOfSales.DataCenter.Application;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Domain.Settings;
using PointOfSales.DataCenter.Infrastructure.Hangfire;
using PointOfSales.DataCenter.Infrastructure.Persistence;
using PointOfSales.DataCenter.Services;
using System;
using System.Collections.Generic;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PointOfSales.DataCenter
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public IConfiguration _configuration { get; }

        public IWebHostEnvironment _environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration from AppSettings
            services.Configure<JwtSecurityTokenSettings>(_configuration.GetSection("JwtSecurityToken"));
            services.Configure<SmtpSettings>(_configuration.GetSection("SmtpSettings"));
            #endregion

            #region Hangfire
            services.ConfigureHangfire(_configuration);
            #endregion

            services.AddHealthChecks().AddSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            //services.AddHealthChecksUI(setupSettings: setup =>
            //{
            //    setup.AddHealthCheckEndpoint("Basic Health Check", $"https://localhost:44348/healthcheck");
            //});
            //DI for Application
            services.AddApplication();

            //DI for Infrastructure.Persitence
            services.ConfigureInfrastructure(_configuration, _environment);
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\PointOfSales.DataCenter.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Point Of Sales - Data Distribution",
                    Description = "This will be the Core of the entire solution (Point Of Sales). This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "Mukesh Murugan",
                        Email = "iammukeshm@gmail.com",
                        Url = new Uri("https://mukeshmurugan.com/contact"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token to access this API",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer",
                            },
                        }, new List<string>()
                    },
                });

            });
            #endregion

            #region Api Versioning
            // Add API Versioning to the Project
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            // Configure hangfire to use the new JobActivator we defined.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PointOfSales Api");
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.DisplayOperationId();
            });
            #endregion


            app.UseHealthChecks("/healthcheck", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
            });
            //app.UseHealthChecksUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
