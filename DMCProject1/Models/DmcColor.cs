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
        public string Name { get; set; }
        public string HexaDecimal { get; set; }

       //public string getHtmlColorString()
        //{

            //return "rgb(Red, Green, Blue)";
        //}

        //public static System.Drawing.Color FromArgb(int Red, int Green, int Blue);
    }    
    
    
}