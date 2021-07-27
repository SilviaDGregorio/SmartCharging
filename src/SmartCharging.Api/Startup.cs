using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartCharging.Api.Mapper;
using SmartCharging.DataAccess;
using SmartCharging.Domain;
using SmartCharging.Domain.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace SmartCharging.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartCharingDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DataBase")));

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IChargeStationService, ChargeStationService>();
            services.AddScoped<IConnectorService, ConnectorService>();
            services.AddScoped<IGroupDomain, GroupDomain>();
            services.AddScoped<IChargeStationDomain, ChargeStationDomain>();
            services.AddScoped<IConnectorDomain, ConnectorDomain>();

            services.AddAutoMapper(typeof(OrganizationProfile));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartCharging.Api", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SmartCharingDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartCharging.Api v1"));
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            dbContext.Database.Migrate();
        }
    }
}
