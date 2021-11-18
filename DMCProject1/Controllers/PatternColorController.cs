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
    public class PatternColorController : Controller
    {
        private DmcContext db = new DmcContext();

        // GET: Pattern
        public ActionResult Index()
        {
            return View(db.PatternColors.ToList());
        }

        // GET: Pattern/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternColor patternColor = db.PatternColors.Find(id);
            if (patternColor == null)
            {
                return HttpNotFound();
            }
            return View(patternColor);
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
        public ActionResult Create([Bind(Include = "PCId,PatternID,DmcId,Numstitches")] PatternColor patternColor)
        {
            if (ModelState.IsValid)
            {
                db.PatternColors.Add(patternColor);
                db.SaveChanges();
                return RedirectToAction("Create", "PatternColor");
            }

            return View(patternColor);
        }

        // GET: Pattern/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternColor patternColor = db.PatternColors.Find(id);
            if (patternColor == null)
            {
                return HttpNotFound();
            }
            return View(patternColor);
        }

        // POST: Pattern/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PCId,PatternID,DmcId,Numstitches")] PatternColor patternColor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patternColor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patternColor);
        }

        // GET: Pattern/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternColor patternColor = db.PatternColors.Find(id);
            if (patternColor == null)
            {
                return HttpNotFound();
            }
            return View(patternColor);
        }

        // POST: Pattern/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatternColor patternColor = db.PatternColors.Find(id);
            db.PatternColors.Remove(patternColor);
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
