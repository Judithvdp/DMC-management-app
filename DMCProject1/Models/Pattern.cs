using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMCProject1.Models
{
    public class Pattern
    {
        public int PatternID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
    }
}
