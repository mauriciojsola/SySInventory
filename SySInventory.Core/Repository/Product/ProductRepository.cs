using SySInventory.Core.Infrastructure.Persistence;

namespace SySInventory.Core.Repository.Product
{
    public class ProductRepository : BaseRepository<Model.Entities.Product>, IProductRepository
    {
        public ProductRepository(IContextProvider objectContextProvider)
            : base(objectContextProvider)
        {
        }
    }

    public interface IProductRepository : IRepository<Model.Entities.Product>
    {
    }

}
