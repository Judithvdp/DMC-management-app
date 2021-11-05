using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMCProject1.Models
{
    public class PatternColor
    {
        public int PCID { get; set; }
        public int PatternID { get; set; }
        public int DmcID { get; set; }
        public int NumStitches { get; set; }
    }
}