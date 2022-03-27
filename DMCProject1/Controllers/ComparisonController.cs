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

        /// <summary>
        /// converts hexadecimal color string to a LabColor object representing the same color
        /// </summary>
        /// <param name="hexadecimal">hexadecimal color string e.g. #000000 for black</param>
        /// <returns>LabColor object representing the same color as the hexadecimal color string</returns>
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
        /// <summary>
        /// Compares the color list of the user to the color list of a pattern and produces a list of colors that are available, a list of colors with alternative colors for the pattern and a list of colors that are unavailable.
        /// Alternative colors are colors that have a difference of 12 or less between them and the original color in CIELAB space.
        /// 
        /// TODO: Add user support. Now only uses usercolors of userid 1.
        /// </summary>
        /// <param name="PatternId"> Id of pattern that is compared to the user color list</param>
        /// <returns>A Comparison object that contains three lists: Owned, AlternativeList and NotOwned</returns>
        public ActionResult Comparison(int PatternId)
        {
            Comparison comparison = new Comparison();
            // TODO?: change list<userColor> to list<DmcColor> for userColors and owned
            List<PatternColor> patternColors = new List<PatternColor>();
            List<UserColor> userColors = new List<UserColor>();
            List<DmcColor> owned = new List<DmcColor>();
            List<DmcColor> notOwned = new List<DmcColor>();
            List<AlternativeContainer> alternatives = new List<AlternativeContainer>();
            List<DmcColor> dmcColors = new List<DmcColor>();

            // retrieve all colors for this pattern
            patternColors = db.PatternColors.Where(e => e.PatternId == PatternId).ToList();
            // retrieve all user colors for this user
            // TODO: Add user support. Now UserId is set to 1
            userColors = db.UserColors.Where(e => e.UserId == 1).ToList();

            dmcColors = db.DmcColors.ToList();

            //Check what to do for each patterncolor
            foreach (PatternColor patternColor in patternColors)
            {
                if (userColors.Where(e => e.DmcId == patternColor.DmcId).Count() > 0)
                {
                    // if the user has this color, it is added to the list of owned colors.
                    owned.Add(dmcColors.Where(e => e.DmcId == patternColor.DmcId).First());
                }
                else if (userColors.Where(e => e.DmcId == patternColor.DmcId).Count() == 0)
                {
                    // if the user does not have the color, we need to check if they have a color that can be used as an alternative.
                    AlternativeContainer alternativeContainer = new AlternativeContainer();
                    alternativeContainer.Alternatives = new List<ColorCollection>();

                    alternativeContainer.Original = dmcColors.Where(e => e.DmcId == patternColor.DmcId).First();

                    var differenceCalculator = new CIEDE2000ColorDifference();
                    var dmcColor = alternativeContainer.Original;
                    var hexadecimal = dmcColor.HexaDecimal;

                    // convert hexadecimal color to labspace.
                    var labColor1 = HexToLab(hexadecimal);

                    // store the difference between the pattern color and each of the usercolors in a dictionary, indexed by DMC Id.
                    Dictionary<int, double> differenceDict = new Dictionary<int, double>();

                    // loop over all user colors and add their distance in CIELAB space to the dictionary if the difference is less than 12.
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

                    // if no user colors are close enough to the patterncolor add the patterncolor to the notOwned list
                    if (differenceDict.Count() < 1)
                    {
                        notOwned.Add(dmcColors.Where(e => e.DmcId == patternColor.DmcId).First());
                    }
                    else
                    {
                        // order the dictionary by ascending difference. So the most similar color is on top.
                        var orderedDictionary = differenceDict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                        // show at most the top 5 most similar alternative colors
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
