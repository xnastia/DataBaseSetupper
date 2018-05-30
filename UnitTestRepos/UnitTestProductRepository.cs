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
        ProductRepository pr;
        Product J17 = new Product { Name = "J17", CategoryId = 1, VendorId = 1, Description = "qwe", Price = 5000 };

        [TestInitialize]
        public void Setup()
        {
            this.pr = new ProductRepository();
            pr.Create(J17);
        }
        [TestMethod]
        public void TestGet()
        {            
            var products = pr.GetProducts();
            int productcount = products.Count();
            Product J=pr.Get(products[productcount - 1].Id);
            Assert.AreEqual(J17.Name,J.Name);
        }
        [TestMethod]
        public void TestDelete()
        {
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
