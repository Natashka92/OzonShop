using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Shop.DataBase;

namespace Shop.HelperDB
{
    public class HelpTableTag
    {
        public List<Tag> GetTagsFromString(String StringOfTags)//, DataContext db)
        {
            using (var db = new DataContext("DBShop"))
            {
                String[] StrTags = StringOfTags.ToLower().Split(new Char[] { ' ' });
                List<Tag> list = new List<Tag>();

                foreach (var tag in StrTags)
                {
                    var tags = db.Tags.Where(s => s.Name == tag);
                    if (tags.Count() == 0)
                    {
                        db.Tags.Add(new Tag { Name = tag, Value = 1 });
                        db.SaveChanges();
                    }
                    else
                    {
                        tags.ToList()[0].Value++;
                        db.SaveChanges();
                    }
                    list.Add(db.Tags.FirstOrDefault(s => s.Name == tag));
                }
                return list;
            }
        }
    }
}
