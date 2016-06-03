using System;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        event EventHandler OnSuccess;
        event EventHandler OnFail;
    }
}
