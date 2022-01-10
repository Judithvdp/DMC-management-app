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
            var DmcColors = new List<DmcColor>();

            
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\judit\source\repos\DMCProject1\DMCProject1\Data\DMC.txt"))
            {
                int dmcId;
                string hexadecimal;
                string name; string[] seperate = line.Split(' ');
                int x = seperate.Count();
                

                // TODO: still need to make exceptionf for ECRU, BLANC etc.
                if (!Int32.TryParse(seperate[0], out dmcId))
                {
                    continue;
                }
                hexadecimal = seperate[1];

                name = "";
                for(int i = 2; i < x; i++)
                {
                    name = name + " " + seperate[i];
                }

                DmcColors.Add(new DmcColor { DmcId = dmcId, Name = name, HexaDecimal = hexadecimal });
            }

            DmcColors.ForEach(s => context.DmcColors.Add(s));
            context.SaveChanges();
        
            var UserColors = new List<UserColor>
            {
                new UserColor{ColorId=1, DmcId=1, UserId=1, Amount=1},
                new UserColor{ColorId=2, DmcId=310, UserId=1, Amount=2},
                new UserColor{ColorId=3, DmcId=780, UserId=1, Amount=5},
                new UserColor{ColorId=4, DmcId=700, UserId=1, Amount=2}
            };

            UserColors.ForEach(s => context.UserColors.Add(s));
            context.SaveChanges();
        }
    }
}
