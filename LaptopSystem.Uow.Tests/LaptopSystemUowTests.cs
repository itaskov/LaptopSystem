using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using LaptopSystem.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace LaptopSystem.Uow.Tests
{
    [TestClass]
    public class LaptopSystemUowTests
    {
        private static TransactionScope tranScope;
        
        private IUowData data;

        private readonly DbContext dbContext;

        [TestInitialize]
        public void TestInit()
        {
            tranScope = new TransactionScope();
        }

        [TestCleanup]
        public void TestTearDown()
        {
            tranScope.Dispose();
        }

        public LaptopSystemUowTests()
        {
            this.dbContext = new ApplicationDbContext();
            this.data = new UowData(this.dbContext);
        }

        [TestMethod]
        public void UowShouldReturnLaptopRepository()
        {
            var laptopRepo = this.data.Laptops;
            Assert.IsNotNull(laptopRepo);
        }

        [TestMethod]
        public void UowShouldReturnManufacturerRepository()
        {
            var manufacturerRepo = this.data.Manufacturers;
            Assert.IsNotNull(manufacturerRepo);
        }

        [TestMethod]
        public void UowShouldReturnCommentRepository()
        {
            var commentRepo = this.data.Comments;
            Assert.IsNotNull(commentRepo);
        }

        [TestMethod]
        public void UowShouldReturnVoteRepository()
        {
            var voteRepo = this.data.Votes;
            Assert.IsNotNull(voteRepo);
        }

        [TestMethod]
        public void Uow_SaveShanges_Should_Return_Zero()
        {
            var expected = this.data.SaveChanges();
            Assert.AreEqual(expected, 0);
        }
    }
}
