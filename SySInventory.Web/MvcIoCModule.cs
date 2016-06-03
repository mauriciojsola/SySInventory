using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using SySInventory.Core.Repository;
using SySInventory.Web.Controllers.Filters;

namespace SySInventory.Web
{
    public class MvcIoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new RepositoryIoCModule { IsWebContext = true, ConnectionStringName = "SySInventoryDb" });

            //Register controllers
            builder.RegisterControllers(ThisAssembly)
                .PropertiesAutowired();

            builder.Register(c => new UnitOfWorkFilter())
                .AsActionFilterFor<Controller>().PropertiesAutowired().InstancePerRequest();

            builder.RegisterFilterProvider();
        }
    }
}