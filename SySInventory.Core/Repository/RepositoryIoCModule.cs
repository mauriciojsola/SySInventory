using Autofac;
using SySInventory.Core.Infrastructure.App;
using SySInventory.Core.Infrastructure.Persistence;

namespace SySInventory.Core.Repository
{
    public class RepositoryIoCModule : Module
    {
        public bool IsWebContext { get; set; }
        public string ConnectionStringName { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            if (IsWebContext)
            {
                builder.RegisterType<WebBasedContextProvider>().As<IContextProvider>().SingleInstance().PropertiesAutowired();
                builder.RegisterType<WebBasedUnitOfWorkProvider>().As<IUnitOfWorkProvider>().SingleInstance().PropertiesAutowired();
            }
            else
            {
                builder.RegisterType<ThreadStaticContextProvider>().As<IContextProvider>().SingleInstance().PropertiesAutowired();
                builder.RegisterType<ThreadStaticUnitOfWorkProvider>().As<IUnitOfWorkProvider>().SingleInstance().PropertiesAutowired();
            }

            builder.Register(c => AppConfiguration.ConfigureOrm<SySInventoryDbContext>(string.Format("name={0}", ConnectionStringName)))
            .SingleInstance().PropertiesAutowired()
            .ExternallyOwned();

            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo<IRepository>().AsClosedTypesOf(typeof(IRepository<>))
                .AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
        }
    }
}
