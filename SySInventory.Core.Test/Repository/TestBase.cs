using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SySInventory.Core.Repository;

namespace SySInventory.Core.Test.Repository
{
    [TestClass]
    public class TestBase
    {
        private static readonly object syncRoot = new object();
        protected static IContainer Container;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            //Configure Repository
            var container = new ContainerBuilder();
            container.RegisterModule(new RepositoryIoCModule { IsWebContext = false, ConnectionStringName = "SySInventoryDb" });
            Container = container.Build();
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {

        }

        [TestCleanup]
        public virtual void TestCleanup()
        {

        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Container.Dispose();
        }

    }
}
