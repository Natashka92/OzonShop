using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Product")]
    public class Product : INamedEntity
    {
        public Product()
        {
            Tags = new HashSet<Tag>();
            Descriptions = new HashSet<Description>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Vendor Vendor { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Picture> Picture { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(13)]
        public string Barcode { get; set; }

        public double Discount { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Parameter> Parameter { get; set; }
        public virtual ICollection<Description> Descriptions { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public static List<Product> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Products.ToList();
            }
        }

        public static List<Product> Select(String searchString, int idTag, int categoryId)
        {
            using (var dbContext = new DataContext())
            {
                var products = new List<Product>();

                if (idTag != 0)
                    products = dbContext.Tags.Include("Products").FirstOrDefault(x => x.TagId == idTag).Products.ToList();
                if (!String.IsNullOrEmpty(searchString))
                    products = dbContext.Products.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                if (categoryId != 0)
                {
                    var listCategory = Category.Select(categoryId);
                    if (Category.IsParentCategory(categoryId))
                    {
                        var category = new List<Category>();
                        foreach (var item in listCategory)
                        {
                            var categories = Category.Select(item.CategoryId);
                            category = category.Concat(categories).ToList();
                            categories.Clear();
                        }
                        listCategory.Clear();
                        listCategory = listCategory.Concat(category).ToList();
                    }

                    foreach (var item in listCategory)
                    {
                        var product = dbContext.Products.Where(s => s.CategoryId == item.CategoryId).ToList();
                        products = products.Concat(product).ToList();
                        product.Clear();
                    }
                }

                return products;
            }
        }

        public static void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                Product product = dbContext.Products.Find(id);
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
        }

        public static Product Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Products.Find(id);
            }
        }

        public static void Edit(Product product)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static void Add(Product product, String StringTags)
        {
            using (var dbContext = new DataContext())
            {
                foreach (var tag in Tag.GetTagsFromString(StringTags))
                {
                    product.Tags.Add(dbContext.Tags.Find(tag.TagId));
                }

                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
        }
    }
}
