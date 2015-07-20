using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaptopSystem.Data;
using LaptopSystem.Models;
using LaptopSystem.Web.Models;
using Microsoft.AspNet.Identity;

namespace LaptopSystem.Web.Controllers
{
    public class LaptopsController : BaseController
    {
        public const int PAGE_SIZE = 4;
        
        public LaptopsController(IUowData data)
            : base(data)
        {

        }
        
        //
        // GET: /Laptops/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: /Laptops/5
        public ActionResult Details(int? id)
        {
            var laptop = this.Data.Laptops.All()
                        .Where(l => l.Id == id)
                        .Select(LaptopDetailsViewModel.FromLaptop).FirstOrDefault();

            return View(laptop);
        }

        [Authorize]
        public ActionResult Vote(int id)
        {
            var userId = User.Identity.GetUserId();
            var userCanVote = !this.Data.Votes.All().Any(v => v.LaptopId == id && v.ApplicationUserId == userId);

            if (userCanVote)
            //if (true)
            {
                this.Data.Votes.Add(
                        new Vote
                        {
                            ApplicationUserId = userId,
                            LaptopId = id
                        });
                this.Data.SaveChanges();
            }

            var votes = this.Data.Laptops.GetById(id).Votes.Count;

            return Content(votes.ToString());
        }

        //public JsonResult Autocomplete(string term)
        //{
        //   var laptops = this.Data.Laptops.All();
        //    var filteredLaptopsName = laptops.Where(l => l.Model.Contains(term)).ToList();
        //    //List<string> test = new List<string>
        //    //{
        //    //    "Ivan",
        //    //    "Taskov"
        //    //};
        //    return Json(filteredLaptopsName.Select(p => new { query = p.Model }), JsonRequestBehavior.AllowGet);
        //}

        [Authorize]
        public ActionResult Search(LaptopSearchModel laptopSearchModel)
        {
            laptopSearchModel.Manufacturers = this.Data.Manufacturers.All().Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString(), Selected = false });
            
            //var laptopViewModel = new LaptopViewModel
            //{
            //    LaptopSearchModel = laptopSearchModel
            //};
            
            if (ModelState.IsValid)
            {
                var page = laptopSearchModel.Page.GetValueOrDefault(1);
                var pages = GetPageNumbers(laptopSearchModel);
                ViewBag.Pages = pages;
                
                laptopSearchModel.Laptops = GetLaptops(laptopSearchModel, page);
                
                return View(laptopSearchModel);
            }
           
            return View(laptopSearchModel);
        }

        private IQueryable<LaptopViewModel> GetLaptops(LaptopSearchModel laptopSearchModel, int page)
        {
            var laptops = this.Data.Laptops.All()
                                            .OrderByDescending(l => l.Votes.Count)
                                            .Where(l => laptopSearchModel.ByModel == null || l.Model.Contains(laptopSearchModel.ByModel))
                                            .Where(l => laptopSearchModel.ByManufacturer == null || l.ManufacturerId == laptopSearchModel.ByManufacturer)
                                            .Where(l => laptopSearchModel.ByPrice == null || l.Price <= laptopSearchModel.ByPrice)
                                            .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                            .Select(LaptopViewModel.FromLaptop);
            return laptops;
        }

        private decimal GetPageNumbers(LaptopSearchModel laptopSearchModel)
        {
            var pages = Math.Ceiling((decimal)this.Data.Laptops.All()
                                                        .Where(l => laptopSearchModel.ByModel == null || l.Model.Contains(laptopSearchModel.ByModel))
                                                        .Where(l => laptopSearchModel.ByManufacturer == null || l.ManufacturerId == laptopSearchModel.ByManufacturer)
                                                        .Where(l => laptopSearchModel.ByPrice == null || l.Price <= laptopSearchModel.ByPrice)
                                                       .Count() / PAGE_SIZE);
            return pages;
        }

        //[Authorize]
        //public ActionResult List(int? id)
        //{
        //    var page = id.GetValueOrDefault(1);
        //    var pages = Math.Ceiling((decimal)this.Data.Laptops.All().Count() / PAGE_SIZE);
        //    ViewBag.Pages = pages;
        //    var laptops = this.Data.Laptops.All()
        //                                    .OrderByDescending(l => l.Votes.Count)
        //                                    .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
        //                                    .Select(LaptopViewModel.FromLaptop);

        //    ViewBag.Manufacturer = this.Data.Manufacturers.All().Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString(), Selected = false });

        //    return View(laptops);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult PostComment(SubmitCommentModel submitCommentModel)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                string userName = User.Identity.GetUserName();

                this.Data.Comments.Add(new Comment
                    {
                        LaptopId = submitCommentModel.LaptopId,
                        Content = submitCommentModel.Comment,
                        ApplicationUserId = userId
                    });

                this.Data.SaveChanges();

                var commentViewModel = new CommentViewModel { Author = userName, Content = submitCommentModel.Comment };

                return PartialView("_Comments", commentViewModel);
            }

            //var message = string.Join(" | ", ModelState.Values
            //            .SelectMany(v => v.Errors)
            //            .Select(e => e.ErrorMessage));
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }
    }
}