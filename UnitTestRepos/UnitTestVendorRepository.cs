using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;


namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestVendorRepository
    {
        VendorRepository vr;

        [TestInitialize]
        public void Setup() {
            this.vr = new VendorRepository();
        }
        [TestMethod]
        public void TestGet()
        {
       
            Vendor Comanche = new Vendor { Name = "Comanche" };
            vr.Create(Comanche);
            var vendors = vr.GetVendors();
            int vendorscount = vendors.Count();
            Vendor C = vr.Get(vendors[vendorscount - 1].Id);
            Assert.AreEqual(Comanche.Name, C.Name);
        }
        [TestMethod]
        public void TestGetVendors()
        {
   
            Vendor Comanche = new Vendor { Name = "Comanche"};
            Vendor Xiaomi = new Vendor { Name = "Xiaomi"};
            vr.Create(Comanche);
            vr.Create(Xiaomi);
            List<Vendor> getvendors = vr.GetVendors();
            int countvendors = getvendors.Count();
            Assert.AreEqual(Comanche.Id, getvendors[countvendors - 2].Id);
            Assert.AreEqual(Xiaomi.Id, getvendors[countvendors - 1].Id);

        }
    }
}

