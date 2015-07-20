using System;
using System.Collections.Generic;
using System.Transactions;
using LaptopSystem.Data;
using LaptopSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using LaptopSystem.Web.Controllers;
using System.Web.Mvc;
using LaptopSystem.Web.Models;

namespace LaptopSystem.Web.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private IUowData data = new UowData();

        [TestMethod]
        public void IndexMethod_ShouldReturn_ProperNumbers_Of_Laptops()
        {
            var laptopList = new List<Laptop>
            {
                new Laptop
                        {
                            HardDiskSize = 500,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "Eye PC",
                            MonitorSize = 15.6,
                            Price = 1600,
                            RamMemorySize = 6,
                            Manufacturer = new Manufacturer{ Name = "Assus"}
                            //Votes = new List<Vote>{ new Vote() }
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "Eye PC2",
                            MonitorSize = 15.6,
                            Price = 1820,
                            RamMemorySize = 8,
                            Manufacturer = new Manufacturer{ Name = "Assus"}
                            //Votes = new List<Vote>{ new Vote() }
                        },
            };
            
            var laptopsRepoMock = new Mock<IRepository<Laptop>>();
            laptopsRepoMock.Setup(x => x.All()).Returns(laptopList.AsQueryable);
            
            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Laptops).Returns(laptopsRepoMock.Object);

            var controller = new HomeController(uowMock.Object);
            var viewResult = controller.Index() as ViewResult;

            Assert.IsNotNull(viewResult, "Index action returns null.");
            var model = viewResult.Model as IEnumerable<LaptopViewModel>;
            Assert.IsNotNull(model, "The model is null");
            Assert.AreEqual(2, model.Count());
        }

    }
}
