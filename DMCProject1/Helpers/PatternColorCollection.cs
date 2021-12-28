using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMCProject1.Helpers
{
    public class PatternColorCollection
    {
        public int PatternId { get; set; }
        public string PatternName { get; set; }
        public int UserId { get; set; }
        
        public List<PatternColorCollectionItem> Items { get; set; }
    }
}