namespace SySInventory.Core.Model.Entities
{
    public class Product : EntityBase
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal RetailPrice { get; set; } // Precio de venta minorista
        public decimal WholesalePrice { get; set; } // Precio de venta mayorista

        public int ProductCategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}
