using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;


namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestOrderItemRepository
    {
        OrderItemRepository oir;
        OrderItem OI = new OrderItem { OrderId = 1, ProductId = 2, Quantity = 3 };
        
        [TestInitialize]
        public void Setup()
        {
            this.oir = new OrderItemRepository();
            oir.Create(OI);
        }
        [TestMethod]
        public void TestDelete()
        {
                     
            var orderitems = oir.GetOrderItems();
            int orderitemscount = orderitems.Count();
            oir.Delete(orderitems[orderitemscount - 1].Id);
            var lastorderitem = oir.GetOrderItems().Last();
            Assert.AreEqual(lastorderitem.Id, OI.Id, "Id should be equal");

        }
        [TestMethod]
        public void TestUpdate()
        {
            var orderitems = oir.GetOrderItems();
            int orderitemscount = orderitems.Count();
            OI.ProductId = 3;
            oir.Update(OI);
            Assert.AreEqual(OI.ProductId, 3, "ProductId should be 3");
        }
    }
    
}


