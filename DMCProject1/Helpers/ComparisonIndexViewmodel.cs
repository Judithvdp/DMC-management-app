using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMCProject1.Models;


namespace DMCProject1.Helpers
{
    public class ComparisonIndexViewmodel
    {
        public int PatternId { get; set; }
        public List<Pattern> Patterns { get; set; }
    }
}