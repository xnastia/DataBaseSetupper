using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestUserRepository : RepositoryBaseTest
    {
        protected UserRepository repo;
        public UnitTestUserRepository() : base() { }
        [TestInitialize]
        public void Setup()
        {
            this.repo = new UserRepository();
            User Ivan = new User { Name = "Ivan", Email = "ivan@mail.com", Password = "qweqwe" };
            repo.Create(Ivan);
        }
        [TestMethod]
        public void TestCreate()
        {
           
            var lastuser = repo.GetUsers().Last();
            Assert.AreEqual("Ivan", lastuser.Name, "Last name should be Ivan");
        }
              

        [TestMethod]
        public void TestGetUsers()
        {
            User Petro = new User { Name = "Petro", Email = "petro@mail.com", Password = "qweqweqwe" };
            repo.Create(Petro);
            List<User> getusers = repo.GetUsers();
            int countusers = getusers.Count();
            Assert.AreEqual(Petro.Id, getusers[countusers-1].Id, "Id should be equal");

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

