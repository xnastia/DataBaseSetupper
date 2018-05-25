using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestUserRepository
    {
        [TestMethod]
        public void TestCreate()
        {
            string conn = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False", @"(localdb)\Projects", "NewBikeStore");
            var db= new StoreDbContext(conn);           
            var ur = new UserRepository(conn);
            User Ivan = new User { Name = "Ivan", Email = "ivan@mail.com", Password = "qweqwe" };
            ur.Create(Ivan);
            var lastuser = ur.GetUsers().Last();
            Assert.AreEqual("Ivan", lastuser.Name);
        }
              

        [TestMethod]
        public void TestGetUsers()
        {
            string conn = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False", @"(localdb)\Projects", "NewBikeStore");
            var db = new StoreDbContext(conn);
            var ur = new UserRepository(conn);
            User Petro = new User { Name = "Petro", Email = "petro@mail.com", Password = "qweqweqwe" };
            User Ivan = new User { Name = "Ivan", Email = "ivan@mail.com", Password = "qweqwe" };
            ur.Create(Petro);
            ur.Create(Ivan);
            List<User> getusers = ur.GetUsers();
            int countusers = getusers.Count();
            Assert.AreEqual(Petro.Id, getusers[countusers-2].Id);
            Assert.AreEqual(Ivan.Id, getusers[countusers-1].Id);

        }
        /*[TestMethod]
        public void TestMockGetUsers()
        {
            User Ivan = new User { Name = "Ivan", Email = "ivan@mail.com", Password = "qweqwe" };

            var mock = new Mock<UserRepository>();
            mock.Setup(a => a.GetUsers()).Returns(new List<User>() {  Ivan  });
            Assert.AreEqual(Ivan.Id,mock.Object.GetUsers().Last().Id); 
        }*/
    }
}

