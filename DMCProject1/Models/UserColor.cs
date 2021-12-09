using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMCProject1.Models
{
    public class UserColor
    {
        [Key]
        public int ColorId { get; set; }
        public int DmcId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
    }
}
