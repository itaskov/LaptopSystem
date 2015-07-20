using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaptopSystem.Models;
using LaptopSystem.Data;

namespace LaptopSystem.Web.Controllers
{
    public class VotesAdministrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /VotesAdministration/
        public ActionResult Index()
        {
            var votes = db.Votes.Include(v => v.Author).Include(v => v.Laptop);
            return View(votes.ToList());
        }

        // GET: /VotesAdministration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Include(v => v.Author).Include(v => v.Laptop).FirstOrDefault(v => v.Id == id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        // GET: /VotesAdministration/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.LaptopId = new SelectList(db.Laptops, "Id", "Model");
            return View();
        }

        // POST: /VotesAdministration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ApplicationUserId,LaptopId")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                db.Votes.Add(vote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", vote.ApplicationUserId);
            ViewBag.LaptopId = new SelectList(db.Laptops, "Id", "Model", vote.LaptopId);
            return View(vote);
        }

        // GET: /VotesAdministration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", vote.ApplicationUserId);
            ViewBag.LaptopId = new SelectList(db.Laptops, "Id", "Model", vote.LaptopId);
            return View(vote);
        }

        // POST: /VotesAdministration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ApplicationUserId,LaptopId")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", vote.ApplicationUserId);
            ViewBag.LaptopId = new SelectList(db.Laptops, "Id", "Model", vote.LaptopId);
            return View(vote);
        }

        // GET: /VotesAdministration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Include(v => v.Author).Include(v => v.Laptop).FirstOrDefault(v => v.Id == id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        // POST: /VotesAdministration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vote vote = db.Votes.Find(id);
            db.Votes.Remove(vote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
