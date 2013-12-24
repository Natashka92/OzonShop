using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public class TagRepository
    {
        public static String[] Select()
        {
            using (var dbContext = new DataContext())
            {
                var tags = dbContext.Tags;
                String[] listTag = new String[tags.Count()];
                int i = 0;

                foreach (var tag in tags)
                {
                    listTag[i] = tag.Name;
                    i++;
                }
                return listTag;
            }
        }

        public static string GetStringTag(int productId)
        {
            using (var dbContext = new DataContext())
            {
                String stringTags = "";
                var tags = dbContext.Products.Include("Tags").FirstOrDefault(x => x.ProductId == productId).Tags.ToList();
                foreach (var tag in tags)
                {
                    stringTags += tag.Name + ",";
                }
                return stringTags;
            }
        }

        public static IEnumerable<TagCloud> CreatorCloud(int ClusterCount)
        {
            using (var dbContext = new DataContext())
            {
                var tagsCloud = dbContext.Tags.AsEnumerable();
                int totalCount = tagsCloud.Count();
                tagsCloud = tagsCloud.OrderBy(s => s.Value);

                List<List<TagCloud>> clusters = new List<List<TagCloud>>();
                if (totalCount > 0)
                {
                    int min = tagsCloud.Min(s => s.Value);
                    int max = tagsCloud.Max(s => s.Value) + min;
                    int completeRange = max - min;
                    double groupRange = (double)completeRange / (double)(ClusterCount);
                    List<TagCloud> cluster = new List<TagCloud>();
                    double currentRange = min + groupRange;
                    for (int i = 0; i < totalCount; i++)
                    {
                        while (tagsCloud.ToArray()[i].Value > currentRange)
                        {
                            clusters.Add(cluster);
                            cluster = new List<TagCloud>();
                            currentRange += groupRange;
                        }
                        var newCloudTag = new TagCloud { Id = tagsCloud.ToArray()[i].TagId, Name = tagsCloud.ToArray()[i].Name, Total = tagsCloud.ToArray()[i].Value };
                        cluster.Add(newCloudTag);
                    }
                    clusters.Add(cluster);
                }
                TagCloud tc;
                List<TagCloud> result = new List<TagCloud>();
                for (int i = 0; i < clusters.Count; i++)
                {
                    foreach (TagCloud item in clusters[i])
                    {
                        tc = new TagCloud(item.Id, item.Name, "tag" + i.ToString(), item.Total);
                        result.Add(tc);
                    }
                }
                return result.OrderBy(x => x.Name).AsEnumerable();
            }
        }

        public static List<Tag> GetTagsFromString(String StringOfTags)
        {
            using (var dbContext = new DataContext())
            {
                String[] StrTags = StringOfTags.ToLower().Split(new Char[] { ',' });
                List<Tag> list = new List<Tag>();

                foreach (var tag in StrTags)
                {
                    var tags = dbContext.Tags.Where(s => s.Name == tag);
                    if (tags.Count() == 0)
                    {
                        dbContext.Tags.Add(new Tag { Name = tag, Value = 1 });
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        tags.ToList()[0].Value++;
                        dbContext.SaveChanges();
                    }
                    list.Add(dbContext.Tags.FirstOrDefault(s => s.Name == tag));
                }
                return list;
            }
        }
    }
}