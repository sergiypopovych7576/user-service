using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Services.Shared.Domain.Interfaces;
using Services.Shared.Exceptions;
using Services.Shared.Extensions;
using Services.Shared.Web.Middleware;
using User.Database;
using User.Service.Configs;
using User.Service.Profiles;

namespace User.Service
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User.Service", Version = "v1" });
            });

            services.AddAutoMapper(typeof(UserMappingProfile));

            services.AddDbContext<ReadUserDbContext>(options => 
                options.UseSqlServer("name=ConnectionStrings:User"));

            services.AddDbContext<WriteUserDbContext>(options =>
               options.UseSqlServer("name=ConnectionStrings:User"));

            services.AddScoped<IReadDbContext, ReadUserDbContext>();
            services.AddScoped<IWriteDbContext, WriteUserDbContext>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DIConfig());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Starting application");
            logger.LogInformation($"Environment: {env.EnvironmentName}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User.Service v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                if (env.IsDevelopment())
                {
                    await context.Response.WriteAsJsonAsync(exception.FormatException());
                    return;
                }

                if (exception.GetType().IsSubclassOf(typeof(UserException)))
                {
                    await context.Response.WriteAsJsonAsync(exception.Message);
                    return;
                }

                await context.Response.WriteAsJsonAsync("Internal Error");
            }));

            app.UseMiddleware<RequestLoggerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
