using System;
using System.Data.Entity;
using System.Transactions;
using LaptopSystem.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LaptopSystem.Web.Controllers;
using LaptopSystem.Web.App_Start;
using LaptopSystem.Models;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace LaptopSystem.LaptopRepository.Tests
{
    [TestClass]
    public class LaptopSystemRepositoriesTests
    {
        private static TransactionScope tranScope;

        private IUowData data = new UowData();

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
        
        [TestMethod]
        public void All_Should_Return_8_Laptops()
        {
            var expected = this.data.Laptops.All().Take(8).Count().ToString();

            Assert.AreEqual(expected, "8", "Laptop's repository All method returns different number of laptops than expected.");
        }

        [TestMethod]
        public void GetById_Should_Return_Selected_Laptops()
        {
            var laptop = new Laptop
            {
                HardDiskSize = 500,
                ImageUrl = @"~/images/laptopImage.jpg",
                Model = "EliteBook 8760w",
                MonitorSize = 15.6,
                Price = 1600,
                RamMemorySize = 6,
                ManufacturerId = 2
            };

            this.data.Laptops.Add(laptop);
            this.data.SaveChanges();

            var expected = this.data.Laptops.GetById(laptop.Id);

            Assert.AreSame(expected, laptop, "Laptop's repository GetById method returns different laptop than expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void Add_Should_Return_DbEntityValidationException_For_Model()
        {
            var laptop = new Laptop
            {
                Model = null,
                HardDiskSize = 500,
                ImageUrl = @"~/images/laptopImage.jpg",
                MonitorSize = 15.6,
                Price = 1600,
                RamMemorySize = 6,
                ManufacturerId = 2
            };

            this.data.Laptops.Add(laptop);
            this.data.SaveChanges();
        }
    }
}
