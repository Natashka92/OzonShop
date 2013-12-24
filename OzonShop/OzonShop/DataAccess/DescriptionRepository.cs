using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;
using System.Data.Entity;

namespace OzonShop.DataAccess
{
    public class DescriptionRepository
    {
        public static void Add(String text, int productId, int userId)
        {
            using (var dbContext = new DataContext())
            {
                var listProduct = new List<Product>();
                
                var comment = new Description() { 
                    TextDescription = text,
                    Data = DateTime.Now,
                    UserId = userId
                };
                var product = dbContext.Products.Find(productId);
                product.Descriptions.Add(comment);

                var user = dbContext.Users.Find(userId);
                user.Descriptions.Add(comment);
                dbContext.Entry(user).State = EntityState.Modified;

                dbContext.Descriptions.Add(comment);
                dbContext.Entry(product).State = EntityState.Modified;                
                dbContext.SaveChanges();
            }
        }
    }
}