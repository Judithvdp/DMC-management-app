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
    public class UserColorController : Controller
    {
        private DmcContext db = new DmcContext();

        // GET: UserColors
        public ActionResult Index()
        {
            return View(db.UserColors.ToList());
        }

        // GET: UserColors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = db.UserColors.Find(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }
            return View(userColor);
        }

        // GET: UserColors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorId,DmcID,UserID,Amount")] UserColor userColor)
        {
            if (ModelState.IsValid)
            {
                db.UserColors.Add(userColor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userColor);
        }

        // GET: UserColors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = db.UserColors.Find(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }
            return View(userColor);
        }

        // POST: UserColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorId,DmcID,UserID,Amount")] UserColor userColor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userColor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userColor);
        }

        // GET: UserColors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = db.UserColors.Find(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }
            return View(userColor);
        }

        // POST: UserColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserColor userColor = db.UserColors.Find(id);
            db.UserColors.Remove(userColor);
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
