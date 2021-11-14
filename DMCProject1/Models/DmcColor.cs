using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMCProject1.Models
{
    public class DmcColor
    {
        [Key]
        public int DmcId { get; set; }
        public string Color { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

    }
}