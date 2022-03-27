using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMCProject1.Models
{
    public class ValdaniColor : AbstractColor
    {
        private string id;

        public int DbId { get; set; }
        public int ValdaniId { get; set; }
        public override string Name { get; set; }
        public override string HexaDecimal { get; set; }

        public override string Id
        {
            get { return "Valdani." + ValdaniId; }
            set { id = value; }
        }
    }
}