using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SySInventory.Core.Infrastructure.Persistence;
using SySInventory.Core.Model.Entities;

namespace SySInventory.Core.Test
{
    [TestClass]
    [Ignore]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new SySInventoryDbContext();
            var products = context.Products.ToList();
            var categories = context.ProductCategories.ToList();

            var p1 = new Product();
            p1.Category = categories.FirstOrDefault();
            p1.Code = Guid.NewGuid().ToString().Substring(0, 12);
            p1.CostPrice = 1;
            p1.RetailPrice = 2;
            p1.WholesalePrice = 3;

            context.Products.Add(p1);
            context.SaveChanges();

        }
    }
}
