using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DMCProject1.DAL;
using DMCProject1.Models;

namespace DMCProject1.Controllers
{
    public class PatternController : Controller
    {
        private DmcContext db = new DmcContext();

        // GET: Pattern
        public ActionResult Index()
        {
            return View(db.Patterns.ToList());
        }

        // GET: Pattern/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pattern pattern = db.Patterns.Find(id);
            if (pattern == null)
            {
                return HttpNotFound();
            }
            return View(pattern);
        }

        // GET: Pattern/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pattern/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatternId,UserId,Name")] Pattern pattern)
        {
            if (ModelState.IsValid)
            {
                db.Patterns.Add(pattern);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pattern);
        }

        // GET: Pattern/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pattern pattern = db.Patterns.Find(id);
            if (pattern == null)
            {
                return HttpNotFound();
            }
            return View(pattern);
        }

        // POST: Pattern/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatternId,UserId,Name")] Pattern pattern)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pattern).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pattern);
        }

        // GET: Pattern/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pattern pattern = db.Patterns.Find(id);
            if (pattern == null)
            {
                return HttpNotFound();
            }
            return View(pattern);
        }

        // POST: Pattern/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pattern pattern = db.Patterns.Find(id);
            db.Patterns.Remove(pattern);
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
