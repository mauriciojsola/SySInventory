using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using SySInventory.Core.Model.Entities;
using SySInventory.Core.Model.Mappings;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public class SySInventoryDbContext : DbContext
    {
        public SySInventoryDbContext() : base("SySInventoryDb")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public SySInventoryDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<SySInventoryDbContext>(null);

            // Use reflection to register all EntityTypeConfiguration derived classes, instead of having 
            // to declare each of them like this:
            //modelBuilder.Configurations.Add(new ProductMapping());
            //modelBuilder.Configurations.Add(new ProductCategoryMapping());
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                  .Where(type => !string.IsNullOrEmpty(type.Namespace))
                  .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                       && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>) && !type.IsAbstract);
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    ChangeTracker.DetectChanges(); // Important!

        //    var ctx = ((IObjectContextAdapter)this).ObjectContext;

        //    var objectStateEntryList = ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added
        //                                                   | EntityState.Modified
        //                                                   | EntityState.Deleted).ToList();

        //    foreach (var entry in objectStateEntryList.Where(entry => !entry.IsRelationship))
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                // write log...
        //                break;
        //            case EntityState.Deleted:
        //                // write log...
        //                break;
        //            case EntityState.Modified:
        //                {
        //                    //
        //                    break;
        //                }
        //        }
        //    }
        //    return base.SaveChanges();
        //}

    }

}
