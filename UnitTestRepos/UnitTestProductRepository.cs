using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;


namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestProductRepository
    {
        [TestMethod]
        public void TestGet()
        {
            string conn = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False", @"(localdb)\Projects", "NewBikeStore");
            var db = new StoreDbContext(conn);
            var pr = new ProductRepository(conn);
            Product J17 = new Product { Name = "J17",CategoryId=1, VendorId=1, Description = "qwe",Price=5000};
            pr.Create(J17);
            var products = pr.GetProducts();
            int productcount = products.Count();
            Product J=pr.Get(products[productcount - 1].Id);
            Assert.AreEqual(J17.Name,J.Name);
        }
        [TestMethod]
        public void TestDelete()
        {
            string conn = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False", @"(localdb)\Projects", "NewBikeStore");
            var db = new StoreDbContext(conn);
            var pr = new ProductRepository(conn);
            Product J17 = new Product { Name = "J17", CategoryId = 1, VendorId = 1, Description = "qwe", Price = 5000 };
            pr.Create(J17);
            Product J19 = new Product { Name = "J19", CategoryId = 1, VendorId = 1, Description = "qwe", Price = 9000 };
            pr.Create(J19);
            var products = pr.GetProducts();
            int productcount = products.Count();
            pr.Delete(products[productcount - 1].Id);
            var lastproduct = pr.GetProducts().Last();
            Assert.AreEqual(lastproduct.Id, J17.Id);

        }
    }
}
