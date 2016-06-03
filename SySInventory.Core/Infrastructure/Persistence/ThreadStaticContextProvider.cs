using System.Data.Entity;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public class ThreadStaticContextProvider : IContextProvider
    {
        private DbContext _currentContext;

        public IContextFactory ContextFactory { private get; set; }

        public bool HasCurrent
        {
            get { return _currentContext != null; }
        }

        public DbContext Current
        {
            get { return (_currentContext = _currentContext ?? ContextFactory.CreateContext()); }
        }

        public void Clear()
        {
            _currentContext = null;
        }
    }
}
