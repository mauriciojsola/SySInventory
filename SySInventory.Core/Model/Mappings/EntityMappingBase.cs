using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SySInventory.Core.Model.Entities;

namespace SySInventory.Core.Model.Mappings
{
    public abstract class EntityMappingBase<T> : EntityTypeConfiguration<T> where T : class, IEntityIdentifiable
    {
        protected EntityMappingBase()
        {
            HasKey(u => u.Id);
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Property(p => p.RowVersion).IsConcurrencyToken();

            if (typeof(IEntityAuditable).IsAssignableFrom(typeof(T)))
            {
                Property(x => ((IEntityAuditable)x).DateCreated)
                    .HasColumnName("DateCreated")
                    .HasColumnType("datetime2")
                    .IsRequired();
            }

            if (typeof(IEntityAuditable).IsAssignableFrom(typeof(T)))
            {
                Property(x => ((IEntityAuditable)x).DateModified)
                    .HasColumnName("DateModified")
                    .HasColumnType("datetime2")
                    .IsRequired();
            }


        }
    }
}
