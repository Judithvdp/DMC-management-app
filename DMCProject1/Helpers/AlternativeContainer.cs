using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMCProject1.Models;
using DMCProject1.Helpers;

namespace DMCProject1.Helpers
{
    public class AlternativeContainer
    {
        public DmcColor Original { get; set; }
        public List<ColorCollection> Alternatives { get; set; }
    }
}