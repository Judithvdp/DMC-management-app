using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMCProject1.Models
{
    public abstract class AbstractColor
    {
        public abstract string Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string HexaDecimal { get; set; }
    }
}