using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SySInventory.Core.Model.Mappings
{
    public abstract class EntityMappingBase<T> : EntityTypeConfiguration<T> where T : class, IIdentifiable
    {
        protected EntityMappingBase()
        {
            HasKey(u => u.Id);
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}
