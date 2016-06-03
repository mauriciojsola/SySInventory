using System;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContextProvider _contextProvider;

        public UnitOfWork(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public void Commit()
        {
            if (!_contextProvider.HasCurrent) return;

            _contextProvider.Current.SaveChanges();

            if (OnSuccess != null)
                OnSuccess(this, new EventArgs());
        }

        public void Rollback()
        {
            if (!_contextProvider.HasCurrent) return;

            if (OnFail != null)
                OnFail(this, new EventArgs());
        }

        public event EventHandler OnSuccess;
        public event EventHandler OnFail;

        public void Dispose()
        {
            if (_contextProvider.HasCurrent)
            {
                _contextProvider.Current.Dispose();
                _contextProvider.Clear();
            }

        }
    }
}
