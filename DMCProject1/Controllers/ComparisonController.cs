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

        public ActionResult Comparison(int PatternId)
        {
            Comparison comparison = new Comparison();
            // TODO: change list<userColor> to list<DmcColor> for userColors and owned
            List<PatternColor> patternColors = new List<PatternColor>();
            List<UserColor> userColors = new List<UserColor>();
            List<DmcColor> owned = new List<DmcColor>();
            List<DmcColor> notOwned = new List<DmcColor>();
            List<AlternativeContainer> alternatives = new List<AlternativeContainer>();
            List<DmcColor> dmcColors = new List<DmcColor>();

            patternColors = db.PatternColors.Where(e => e.PatternId == PatternId).ToList();
            // UserId is set to 1
            userColors = db.UserColors.Where(e => e.UserId == 1).ToList();

            dmcColors = db.DmcColors.ToList();

            foreach (PatternColor patternColor in patternColors)
            {
                if (userColors.Where(e => e.DmcId == patternColor.DmcId).Count() > 0)
                {
                    // TODO:
                    owned.Add(dmcColors.Where(e => e.DmcId == patternColor.DmcId).First());
                }
                else if (userColors.Where(e => e.DmcId == patternColor.DmcId).Count() == 0)
                {
                    AlternativeContainer alternativeContainer = new AlternativeContainer();
                    alternativeContainer.Alternatives = new List<ColorCollection>();

                    alternativeContainer.Original = dmcColors.Where(e => e.DmcId == patternColor.DmcId).First();

                    var differenceCalculator = new CIEDE2000ColorDifference();
                    var dmcColor = alternativeContainer.Original;
                    var hexadecimal = dmcColor.HexaDecimal;

                    var labColor1 = HexToLab(hexadecimal);

                    Dictionary<int, double> differenceDict = new Dictionary<int, double>();

                    foreach (UserColor userColor in userColors)
                    {
                        var dmc = dmcColors.Where(e => e.DmcId == userColor.DmcId).First();
                        var hex = dmc.HexaDecimal;
                        var labColor2 = HexToLab(hex);

                        double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2);
                        // difference is determined at around 12 when comparing other dmc thread comparison sites
                        if (difference < 12)
                        {
                            differenceDict.Add(dmc.DmcId, difference);
                        }
                    }

                    if (differenceDict.Count() < 1)
                    {
                        notOwned.Add(dmcColors.Where(e => e.DmcId == patternColor.DmcId).First());
                    }
                    else
                    {
                        var orderedDictionary = differenceDict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                        for (int i = 0; i < Math.Min(5, orderedDictionary.Count); i++)
                        {
                            int dmcId = orderedDictionary.ElementAt(i).Key;
                            DmcColor alternativeColor = dmcColors.Where(e => e.DmcId == dmcId).First();
                            ColorCollection alternativeColorCollection = new ColorCollection()
                            {
                                DmcId = alternativeColor.DmcId,
                                HexaDecimal = alternativeColor.HexaDecimal,
                                Name = alternativeColor.Name
                            };
                            alternativeContainer.Alternatives.Add(alternativeColorCollection);
                        }
                        alternatives.Add(alternativeContainer);
                    }
                }
            }

            comparison.Owned = owned;
            comparison.AlternativeList = alternatives;
            comparison.NotOwned = notOwned;
            return View(comparison);
        }
    }
}
