using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMCProject1.Models
{
    public class DmcColor : AbstractColor
    {
        private string id;

        [Key]
        public int DbId { get; set; }
        public int DmcId { get; set; }
        public override string Name { get; set; }
        public override string HexaDecimal { get; set; }

        public override string Id 
        { 
            get { return "DMC." + DmcId; } 
            set { id = value; }
        }
    }    
    
    
}