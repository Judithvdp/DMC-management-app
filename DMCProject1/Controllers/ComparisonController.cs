using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMCProject1.DAL;
using DMCProject1.Helpers;
using DMCProject1.Models;
using Colourful;
using System.Drawing;

namespace DMCProject1.Controllers
{
    public class ComparisonController : Controller
    {
        private DmcContext db = new DmcContext();
        // GET: Comparison
        public ActionResult Index()
        {
            ComparisonIndexViewmodel viewmodel = new ComparisonIndexViewmodel();
            viewmodel.Patterns = db.Patterns.ToList();
            return View(viewmodel);
        }

        // GET: Comparison/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comparison/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comparison/Create
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

        // GET: Comparison/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comparison/Edit/5
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

        // GET: Comparison/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comparison/Delete/5
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

        private LabColor HexToLab(string hexadecimal)
        {
            Color color = ColorTranslator.FromHtml(hexadecimal);
            // RGB has to be between 0 and 1
            double r = Convert.ToDouble(color.R) / 255;
            double g = Convert.ToDouble(color.G) / 255;
            double b = Convert.ToDouble(color.B) / 255;
            var rgbColor = new RGBColor(r, g, b);
            var rgbToLab = new ConverterBuilder().FromRGB().ToLab().Build();
            var labColor = rgbToLab.Convert(rgbColor);

            return labColor;
        }

        public ActionResult Comparison()
        {
            //int PatternId = 1;
            Comparison comparison = new Comparison();

            List<PatternColor> patternColors = new List<PatternColor>();
            List<UserColor> userColors = new List<UserColor>();
            List<UserColor> owned = new List<UserColor>();
            List<DmcColor> notOwned = new List<DmcColor>();
            List<DmcColor> alternative = new List<DmcColor>();
            List<DmcColor> dmcColors = new List<DmcColor>();

            // PatternId is set to 1
            patternColors = db.PatternColors.Where(e => e.PatternId == 1).ToList();
            // UserId is set to 1
            userColors = db.UserColors.Where(e => e.UserId == 1).ToList();

            dmcColors = db.DmcColors.ToList();

            foreach (PatternColor patternColor in patternColors)
            {
                if (userColors.Where(e => e.DmcId == patternColor.DmcId).Count() > 0)
                {
                    owned.Add(userColors.Where(e => e.DmcId == patternColor.DmcId).First());
                }
                else if (userColors.Where(e => e.DmcId == patternColor.DmcId).Count() == 0)
                {
                    var differenceCalculator = new CIEDE2000ColorDifference();
                    var dmcColor = dmcColors.Where(e => e.DmcId == patternColor.DmcId).First();
                    var hexadecimal = dmcColor.HexaDecimal;

                    var labColor1 = HexToLab(hexadecimal);

                    Dictionary<int, double> differenceDict = new Dictionary<int, double>();

                    foreach (UserColor userColor in userColors)
                    {
                        var dmc = dmcColors.Where(e => e.DmcId == userColor.DmcId).First();
                        var hex = dmc.HexaDecimal;
                        var labColor2 = HexToLab(hex);

                        double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2);
                        // TODO: still need to add a max difference for difference before differences are put in de dictionary
                        differenceDict.Add(dmc.DmcId, difference);
                    }

                    var orderedDictionary = differenceDict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                    
                    for(int i = 0; i < Math.Min(5, orderedDictionary.Count); i++)
                    {
                        int dmcId = orderedDictionary.ElementAt(i).Key;
                        DmcColor alternativeColor = dmcColors.Where(e => e.DmcId == dmcId).First();
                        alternative.Add(alternativeColor);
                    }
                }
                else
                {
                    notOwned.Add(dmcColors.Where(e => e.DmcId == patternColor.DmcId).First());
                }
            }

            comparison.Owned = owned;
            return View(comparison);
        }
    }
}
