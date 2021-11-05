using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMCProject1.Models
{
    public class PatternColor
    {
        public int PCId { get; set; }
        public int PatternId { get; set; }
        public int DmcId { get; set; }
        public int NumStitches { get; set; }
    }
}