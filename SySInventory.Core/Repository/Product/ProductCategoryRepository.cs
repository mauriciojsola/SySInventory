using SySInventory.Core.Infrastructure.Persistence;

namespace SySInventory.Core.Repository.Product
{
    public class ProductCategoryRepository : BaseRepository<Model.Entities.ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IContextProvider objectContextProvider)
            : base(objectContextProvider)
        {
        }
    }

    public interface IProductCategoryRepository : IRepository<Model.Entities.ProductCategory>
    {
    }

}
