using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Shared.Infrastructure.Contexts;
using Services.Shared.Web.Middleware;
using System.Text;
using User.Database;
using User.Service.Configs;
using User.Service.Profiles;
using User.Service.Startups;
using Services.Shared.Web.Loggers;
using User.Service.API.Configs;

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User.Service v1"));
            }
            else
            {
                var fileLoggerConfig = new FileLoggerConfig();
                Configuration.GetSection("Logging").GetSection("File").Bind(fileLoggerConfig);
                loggerFactory.AddFile(fileLoggerConfig.Path);
            }

            logger.LogInformation("Starting application");
            logger.LogInformation($"Environment: {env.EnvironmentName}");

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
            var secretKeyConfig = new SecretKeyConfig();
            Configuration.GetSection(nameof(SecretKeyConfig)).Bind(secretKeyConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Authorization.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = Authorization.AUDIENCE,

                        ValidateLifetime = true,
                        IssuerSigningKey = Authorization.CreateKey(secretKeyConfig.Key),

                        ValidateIssuerSigningKey = true
                    };
                });
        }

        private void ConfigureConfigs(IServiceCollection services)
        {
            services.Configure<SecretKeyConfig>(Configuration.GetSection(nameof(SecretKeyConfig)));
        }
    }
}
