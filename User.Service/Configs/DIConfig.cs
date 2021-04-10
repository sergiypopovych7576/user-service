using Autofac;
using Services.Shared.Infrastructure;
using Services.Shared.Services.Hash;
using Services.Shared.Services.Repositories;
using User.Service.Services.Token;
using User.Services;

namespace User.Service.Configs
{
    public class DIConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(new[] {
                typeof(UserService).Assembly,
                typeof(HashService).Assembly,
                typeof(TokenService).Assembly
            }).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ReadRepository<>))
                .As(typeof(IReadRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(WriteRepository<>))
               .As(typeof(IWriteRepository<>)).InstancePerLifetimeScope();
        }
    }
}
