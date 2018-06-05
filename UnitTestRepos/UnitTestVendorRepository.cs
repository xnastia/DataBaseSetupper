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
        Vendor Comanche = new Vendor { Name = "Comanche" };
        
        [TestInitialize]
        public void Setup() {
            this.vr = new VendorRepository();
            vr.Create(Comanche);
        }
        [TestMethod]
        public void TestGet()
        {
       
            var vendors = vr.GetVendors();
            int vendorscount = vendors.Count();
            Vendor C = vr.Get(vendors[vendorscount - 1].Id);
            Assert.AreEqual(Comanche.Name, C.Name, "Name should be equal");
        }
        [TestMethod]
        public void TestGetVendors()
        {
   
            Vendor Xiaomi = new Vendor { Name = "Xiaomi"};
            vr.Create(Comanche);
            vr.Create(Xiaomi);
            List<Vendor> getvendors = vr.GetVendors();
            int countvendors = getvendors.Count();
            Assert.AreEqual(Comanche.Id, getvendors[countvendors - 2].Id, "Id should be equal");
            
        }
    }
}

