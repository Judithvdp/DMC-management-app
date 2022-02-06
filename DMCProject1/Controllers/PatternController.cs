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
using DMCProject1.Helpers;

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
            PatternColorCollection colorCollections = new PatternColorCollection();

            List<PatternColorCollectionItem> items = new List<PatternColorCollectionItem>();
            List<PatternColor> patternColors = new List<PatternColor>();
            colorCollections.PatternName = db.Patterns.Where(e => e.PatternId == id).First().Name;
            patternColors = db.PatternColors.Where(e => e.PatternId == id).ToList();
            foreach (PatternColor item in patternColors)
            {
                PatternColorCollectionItem color = new PatternColorCollectionItem();
                color.PCId = item.PCId;
                color.DmcId = item.DmcId;
                color.NumStitches = item.NumStitches;

                DmcColor dmcColor = db.DmcColors.Where(d => d.DmcId == item.DmcId).FirstOrDefault();
                if (dmcColor != null)
                {
                    color.HexaDecimal = dmcColor.HexaDecimal;
                    color.Name = dmcColor.Name;
                }

                items.Add(color);
            }

            colorCollections.Items = items;

            return View(colorCollections);
        }
        /*public ActionResult Details(int? id)
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
        } */

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
                return RedirectToAction("Edit", "Pattern", new { id = pattern.PatternId });
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
            PatternCollection patternCollection = new PatternCollection();
            patternCollection.pattern = db.Patterns.Find(id);

            List<PatternColorCollectionItem> collectionItems = new List<PatternColorCollectionItem>();
            List<PatternColor> patternColors = new List<PatternColor>();
            patternColors = db.PatternColors.Where(x => x.PatternId == id).ToList();
            
            foreach(PatternColor item in patternColors)
            {
                PatternColorCollectionItem color = new PatternColorCollectionItem();
                color.DmcId = item.DmcId;
                color.PCId = item.PCId;
                color.NumStitches = item.NumStitches;
                DmcColor dmcColor = new DmcColor();
                dmcColor = db.DmcColors.Where(x => x.DmcId == item.DmcId).FirstOrDefault();
                color.HexaDecimal = dmcColor == null ? "" : dmcColor.HexaDecimal;
                
                collectionItems.Add(color);
            }

            patternCollection.colors = collectionItems;

            if (patternCollection == null)
            {
                return HttpNotFound();
            }
            return View(patternCollection);
        }
        [HttpPost]
        public ActionResult AddColor(int patternId)
        {
            PatternColor newPatternColor = new PatternColor
            {
                DmcId = 0,
                NumStitches = 0,
                PatternId = patternId,
            };

            db.PatternColors.Add(newPatternColor);
            db.SaveChanges();

            DmcColor dmcColor = db.DmcColors.Where(d => d.DmcId == newPatternColor.DmcId).FirstOrDefault();
            string hexa = dmcColor == null ? "" : dmcColor.HexaDecimal;
            string name = dmcColor == null ? "" : dmcColor.Name;

            PatternColorCollectionItem item = new PatternColorCollectionItem()
            {
                DmcId = newPatternColor.DmcId,
                HexaDecimal = hexa,
                Name = name,
                NumStitches = newPatternColor.NumStitches,
                PCId = newPatternColor.PCId
            };

            return PartialView("ColorRow", item);
        }

        // POST: Pattern/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatternCollection patternCollection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(db.Patterns.Find(patternCollection.pattern.PatternId)).State = EntityState.Modified;

                foreach (PatternColorCollectionItem element in patternCollection.colors)
                {
                    int PCId = element.PCId;
                    int Pid = patternCollection.pattern.PatternId;
                    //element.PatternId = Pid;
                    if (db.PatternColors.Find(PCId) != null)
                    {
                        db.PatternColors.Find(PCId).DmcId = element.DmcId;
                        db.PatternColors.Find(PCId).NumStitches = element.NumStitches;
                        db.Entry(db.PatternColors.Find(PCId)).State = EntityState.Modified;
                    }
                    //else db.PatternColors.Add(element);
                }
            }

            db.SaveChanges();

            List<PatternColor> existing = new List<PatternColor>();
            existing = db.PatternColors.Where(e => e.PatternId == patternCollection.pattern.PatternId).ToList();
            foreach (PatternColor item in existing)
            {
                if (patternCollection.colors.Where(e => e.PCId == item.PCId).Count() == 0 ||
                    item.DmcId == 0 ||
                    item.NumStitches == 0)
                {
                    db.PatternColors.Remove(item);
                }
            }
            db.SaveChanges();
            
            return RedirectToAction("Index");
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
