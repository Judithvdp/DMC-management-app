using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DMCProject1.Models;

namespace DMCProject1.Helpers
{
    public class Comparison
    {
        [Key]
        public List<UserColor> Owned { get; set; }
        public List<DmcColor> NotOwned { get; set; }
        public List<DmcColor> Alternative { get; set; }

    }
}