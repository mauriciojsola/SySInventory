using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SySInventory.Core.Repository.Product;

namespace SySInventory.Core.Test.Repository.Product
{
    [TestClass]
    [Ignore]
    public class ProductRepositoryTests : RepositoryTestBase<IProductRepository>
    {
        [TestMethod]
        public void TestGetProduct()
        {
            Model.Entities.Product product = null;
            var code = Guid.NewGuid().ToString().Substring(0, 12);

            //InUnitOfWork(() =>
            //{
            //    var p1 = Repository.GetById(1);
            //    p1.Code = code;
            //});

            //InUnitOfWork(() =>
            //{
            //    product = Repository.GetById(1);
            //});

            var p1 = Repository.GetById(1);
            p1.Code = code;
            product = Repository.GetById(1);

            Assert.AreEqual(code, product.Code);
        }

    }
}
