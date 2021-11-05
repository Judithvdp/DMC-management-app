using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DMCProject1.Models;

namespace DMCProject1.DAL
{
    public class DmcInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DmcContext>
    {
        protected override void Seed(DmcContext context)
        {
            var DmcColors = new List<DmcColor>
            {
                new DmcColor{DmcId=1, Color="red"},
                new DmcColor{DmcId=310,Color="black"}
            };

            DmcColors.ForEach(s => context.DmcColors.Add(s));
            context.SaveChanges();
        
            var UserColors = new List<UserColor>
            {
                new UserColor{ColorId=1, DmcId=1, UserId=1, Amount=1},
                new UserColor{ColorId=2, DmcId=310, UserId=1, Amount=2},
                new UserColor{ColorId=3, DmcId=780, UserId=1, Amount=5}
            };

            UserColors.ForEach(s => context.UserColors.Add(s));
            context.SaveChanges();
        }
    }
}
