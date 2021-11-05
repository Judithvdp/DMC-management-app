using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMCProject1.Models
{
    public class Pattern
    {
        [Key]
        public int PatternId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
