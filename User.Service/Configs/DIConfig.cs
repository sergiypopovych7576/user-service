using Autofac;
using Services.Shared.Domain.Interfaces;
using Services.Shared.Infrastructure;
using Services.Shared.Services.Hash;
using User.Services;

namespace User.Service.Configs
{
    public class DIConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(new[] {
                typeof(RegisterService).Assembly,
                typeof(HashService).Assembly
            }).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ReadRepository<>))
                .As(typeof(IReadRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(WriteRepository<>))
               .As(typeof(IWriteRepository<>)).InstancePerLifetimeScope();
        }
    }
}
