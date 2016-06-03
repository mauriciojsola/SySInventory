using System;
using System.Data.Entity;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public class ContextFactory<T> : IContextFactory where T : DbContext
    {
        private readonly Func<T> _contextCreator;

        public ContextFactory(Func<T> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public DbContext CreateContext()
        {
            return _contextCreator();
        }
    }
}
