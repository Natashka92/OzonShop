using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;
using System.Data.Entity;

namespace OzonShop.DataAccess
{
    public class ProductRepository : IRepositoryProduct
    {
        public List<Product> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Products.ToList();
            }
        }

        public List<Description> SelectCommets(int productId)
        {
            using (var dbContext = new DataContext())
            {
                var product = dbContext.Products.Find(productId);
                return product.Descriptions.ToList();
            }
        }

        public List<Product> Select(String searchString, int idTag, int categoryId)
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
                    var listCategory = new CategoryRepository().Select(categoryId);
                    if (new CategoryRepository().IsParentCategory(categoryId))
                    {
                        var category = new List<Category>();
                        foreach (var item in listCategory)
                        {
                            var categories = new CategoryRepository().Select(item.CategoryId);
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

        public void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                Product product = dbContext.Products.Find(id);
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
        }

        public Product Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Products.Find(id);
            }
        }

        public void Edit(Product product)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void Add(Product product, String StringTags)
        {
            using (var dbContext = new DataContext())
            {
                foreach (var tag in TagRepository.GetTagsFromString(StringTags))
                {
                    product.Tags.Add(dbContext.Tags.Find(tag.TagId));
                }

                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
        }
    }
}