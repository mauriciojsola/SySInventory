namespace SySInventory.Core.Infrastructure.Persistence
{
    public class ThreadStaticUnitOfWorkProvider : IUnitOfWorkProvider
    {
        public IContextProvider ContextProvider { private get; set; }

        public IUnitOfWork BeginUnitOfWork()
        {
            ContextProvider.Clear();
            return Current = new UnitOfWork(ContextProvider);
        }

        public IUnitOfWork Current { get; private set; }
    }

    public interface IUnitOfWorkProvider
    {
        IUnitOfWork BeginUnitOfWork();
        IUnitOfWork Current { get; }
    }
}
