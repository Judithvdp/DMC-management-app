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
    public class DmcColorController : Controller
    {
        private DmcContext db = new DmcContext();

        // GET: DmcColor
        public ActionResult Index()
        {
            return View(db.DmcColors.ToList());
        }

        // GET: DmcColor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DmcColor dmcColor = db.DmcColors.Find(id);
            if (dmcColor == null)
            {
                return HttpNotFound();
            }
            return View(dmcColor);
        }

        // GET: DmcColor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DmcColor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DmcId,Color")] DmcColor dmcColor)
        {
            if (ModelState.IsValid)
            {
                db.DmcColors.Add(dmcColor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dmcColor);
        }

        // GET: DmcColor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DmcColor dmcColor = db.DmcColors.Find(id);
            if (dmcColor == null)
            {
                return HttpNotFound();
            }
            return View(dmcColor);
        }

        // POST: DmcColor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DmcId,Color")] DmcColor dmcColor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dmcColor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dmcColor);
        }

        // GET: DmcColor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DmcColor dmcColor = db.DmcColors.Find(id);
            if (dmcColor == null)
            {
                return HttpNotFound();
            }
            return View(dmcColor);
        }

        // POST: DmcColor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DmcColor dmcColor = db.DmcColors.Find(id);
            db.DmcColors.Remove(dmcColor);
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
