using System.Data.Entity;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public interface IContextFactory
    {
        DbContext CreateContext();
    }
}
