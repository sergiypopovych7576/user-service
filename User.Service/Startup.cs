using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Shared.Domain.Interfaces;
using Services.Shared.Web.Middleware;
using System.Text;
using User.Database;
using User.Service.Configs;
using User.Service.Profiles;
using User.Service.Startups;

namespace User.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            ConfigureAuthentication(services);
            ConfigureConfigs(services);
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

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                await ExceptionHandler.Handle(context, env);
            }));

            app.UseMiddleware<RequestLoggerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var config = new SecretKeyConfig();
            Configuration.GetSection(nameof(SecretKeyConfig)).Bind(config);

            services.AddAuthentication("Bearer")
             .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Authorization.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = Authorization.AUDIENCE,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key))
                    };
                });
        }

        private void ConfigureConfigs(IServiceCollection services)
        {
            services.Configure<SecretKeyConfig>(Configuration.GetSection(nameof(SecretKeyConfig)));
        }
    }
}
