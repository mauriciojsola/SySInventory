using SySInventory.Core.Model.Entities;

namespace SySInventory.Core.Model.Mappings
{
    public class ProductMapping : EntityMappingBase<Product>
    {
        public ProductMapping()
        {
            ToTable("Product");

            Property(u => u.Code)
                .HasColumnName("Code")
                .HasMaxLength(15)
                .IsRequired();

            Property(u => u.Description)
                .HasColumnName("Description")
                .HasMaxLength(50);

            Property(u => u.CostPrice)
                .HasColumnName("CostPrice")
                .HasColumnType("money");

            Property(u => u.RetailPrice)
               .HasColumnName("RetailPrice")
               .HasColumnType("money");

            Property(u => u.WholesalePrice)
               .HasColumnName("WholesalePrice")
               .HasColumnType("money");

            HasRequired(x => x.Category);
        }
    }
}
