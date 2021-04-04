using Autofac;
using Services.Shared.Domain.Interfaces;
using Services.Shared.Infrastructure;
using Services.Shared.Services.Hash;
using User.Service.Services.UserSign;
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
                typeof(UserSignService).Assembly
            }).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ReadRepository<>))
                .As(typeof(IReadRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(WriteRepository<>))
               .As(typeof(IWriteRepository<>)).InstancePerLifetimeScope();
        }
    }
}
