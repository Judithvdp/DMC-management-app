using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMCProject1.Helpers
{
    public class PatternColorCollection
    {
        public int PatternId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int PCId { get; set; }
        public int DmcId { get; set; }
        public int NumStitches { get; set; }
        public string HexaDecimal { get; set; }
    }
}