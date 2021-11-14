using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMCProject1.Controllers
{
    public class PatternColorController : Controller
    {
        // GET: PatternColor
        public ActionResult Index()
        {
            return View();
        }

        // GET: PatternColor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatternColor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatternColor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PatternColor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatternColor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PatternColor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatternColor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
