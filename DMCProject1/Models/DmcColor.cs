using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMCProject1.Models
{
    public class DmcColor
    {
        [Key]
        public int DbId { get; set; }
        public int DmcId { get; set; }
        public string Name { get; set; }
        public string HexaDecimal { get; set; }
    }    
    
    
}