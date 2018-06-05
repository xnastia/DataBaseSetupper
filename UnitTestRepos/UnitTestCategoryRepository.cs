using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;


namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestCategoryRepository
    {
        CategoryRepository cr;
        Category Bike = new Category { Name = "Bike"};

        [TestInitialize]
        public void Setup()
        {
            this.cr = new CategoryRepository();
            cr.Create(Bike);
        }
        [TestMethod]
        public void TestGet()
        {
            var categories = cr.GetCategories();
            int categoriescount = categories.Count();
            Category B = cr.Get(categories[categoriescount - 1].Id);
            Assert.AreEqual(Bike.Name, B.Name, "Names should be equal");
        }
        [TestMethod]
        public void TestDelete()
        {
            Category ElectroBike = new Category { Name = "ElectroBike" };
            cr.Create(ElectroBike);
            var categories = cr.GetCategories();
            int categoriescount = categories.Count();
            cr.Delete(categories[categoriescount - 1].Id);
            var lastcategory = cr.GetCategories().Last();
            Assert.AreEqual(lastcategory.Id, Bike.Id, "Id should be equal");

        }
    }
}
