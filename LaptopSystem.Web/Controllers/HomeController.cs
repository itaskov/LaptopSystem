using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaptopSystem.Web.Models;
using LaptopSystem.Data;

namespace LaptopSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUowData data)
            : base(data)
        {

        }

        //[OutputCache(Duration=0)]
        public ActionResult Index()
        {
            var laptopsByVotes = this.Data.Laptops.All()
                                         .OrderByDescending(l => l.Votes.Count())
                                         .Take(6)
              .Select(LaptopViewModel.FromLaptop).ToList();
            //var laptopsByVotes = this.Data.Laptops.All().Select(LaptopViewModel.FromLaptop).ToList();


            return View(laptopsByVotes);
        }
    }
}