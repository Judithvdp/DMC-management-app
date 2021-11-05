using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMCProject1.Models
{
    public class UserColor
    {
        public int ColorID { get; set; }
        public int DmcID { get; set; }
        public int UserID { get; set; }
        public int Amount { get; set; }
    }
}
