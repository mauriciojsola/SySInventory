using SySInventory.Core.Model.Entities;

namespace SySInventory.Core.Model.Mappings
{
    public class ProductCategoryMapping : EntityMappingBase<ProductCategory>
    {
        public ProductCategoryMapping()
        {
            ToTable("ProductCategory");

            Property(u => u.Name)
                 .HasColumnName("Name")
                 .IsRequired();
        }
    }
}
