using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;


namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestOrderrRepository
    {
        OrderRepository or;
        Order Ord = new Order { Name = "Petro", Phone = "+380996001476", OrderDetails = "qweqwe", Sum = 10000 };
        [TestInitialize]
        public void Setup()
        {
            this.or = new OrderRepository();
            or.Create(Ord);
        }
        [TestMethod]
        public void TestGet()
        {          
            
            var orders = or.GetOrders();
            int orderscount = orders.Count();
            Order Or = or.Get(orders[orderscount - 1].Id);
            Assert.AreEqual(Ord.Id, Or.Id, "Id should be equal");
        }
        [TestMethod]
        public void TestGetOrders()
        {

            Order Ordd = new Order { Name = "Ivan", Phone = "+380634273146", OrderDetails = "qweqwe", Sum = 15000 };
            or.Create(Ordd);
            List<Order> getorders = or.GetOrders();
            int countorders = getorders.Count();
            Assert.AreEqual(Ord.Id, getorders[countorders - 2].Id, "Id should be equal");

        }
    }
}


