﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace UnitTestRepos
{
    [TestClass]
    public class UnitTestCommentRepository
    {
        [TestMethod]
        public void TestGetComments()
        {
            string conn = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False", @"(localdb)\Projects", "NewBikeStore");
            var db = new StoreDbContext(conn);
            var cr = new CommentRepository(conn);
            Comment C = new Comment { Message = "qwe", UserName = "Ivan", ProductId = 1, Rating = 3 };
            Comment CC = new Comment { Message = "qweqwe", UserName = "Petro", ProductId = 1, Rating = 4 };
            Comment CCC = new Comment { Message = "qweqweQwe", UserName = "Petro", ProductId = 2, Rating = 4 };
            cr.Create(C);
            cr.Create(CC);
            cr.Create(CCC);
            List<Comment> getcomments = cr.GetComments(C.ProductId);
            int countcomments = getcomments.Count();
            Assert.AreEqual(C.Id, getcomments[countcomments - 2].Id);
            Assert.AreEqual(CC.Id, getcomments[countcomments - 1].Id);

        }
 
        [TestMethod]
        public void TestMockAverageRating()
        {
            Comment C = new Comment { Message = "qwe", UserName = "Ivan", ProductId = 1, Rating = 3 };
            Comment CC = new Comment { Message = "qweqwe", UserName = "Petro", ProductId = 1, Rating = 4 };
            var mock = new Mock<CommentRepository>();
            mock.Setup(a => a.GetComments(C.ProductId)).Returns(new List<Comment>() { C, CC });
            Assert.AreEqual( (C.Rating + CC.Rating) / 2.0, CommentRepository.AverageRating(mock.Object,C.ProductId));
        }
    }
}
