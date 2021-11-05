using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DMCProject1.Models;

namespace DMC_MVC.DAL
{
    public class DmcInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DmcContext>
    {
        protected override void Seed(DmcContext context)
        {
            var DmcColors = new List<DmcColor>
            {
                new DmcColor{DmcID=1, Color="red"},
                new DmcColor{DmcID=310,Color="black"}
            };

            DmcColors.ForEach(s => context.DmcColors.Add(s));
            context.SaveChanges();
        }
    }
}