using System.Data.Entity;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public interface IContextProvider
    {
        bool HasCurrent { get; }
        DbContext Current { get; }
        void Clear();
    }
}
