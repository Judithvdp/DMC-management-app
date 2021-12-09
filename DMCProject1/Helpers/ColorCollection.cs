using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMCProject1.Models;

namespace DMCProject1.Helpers
{
    public class ColorCollection
    {
        public int ColorId { get; set; }
        public int DmcId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string HexaDecimal { get; set; }

    }
}