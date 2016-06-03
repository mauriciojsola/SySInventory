using System;
using System.Data.Entity;
using SySInventory.Core.Infrastructure.Persistence;

namespace SySInventory.Core.Infrastructure.App
{
    public class AppConfiguration
    {
        public static IContextFactory ConfigureOrm<T>() where T : DbContext, new()
        {
            return new ContextFactory<T>(() => new T());
        }

        public static IContextFactory ConfigureOrm<T>(string connectionString) where T : DbContext, new()
        {
            return new ContextFactory<T>(() => (T)Activator.CreateInstance(typeof(T), connectionString));
        }
    }

    public enum Environment
    {
        Web,
        None
    }
}
