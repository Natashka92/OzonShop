using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;
//using System.Data.Entity;

namespace OzonShop.DataAccess
{
    public class BasketRepository
    {
        public static void Add(Basket basket)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Baskets.Add(basket);
                dbContext.SaveChanges();
            } 
        }

        public static List<Product> Select(int userId)
        {
            using (var dbContext = new DataContext())
            {
                var productInBasket = dbContext.Baskets.Where(s=> s.UserId == userId).ToList();
                List<Product> products = new List<Product>();
                foreach(var item in productInBasket)
                {
                    products.Add(dbContext.Products.Find(item.ProductId));
                }
                return products;
            }
        }

        public static void Delete(int productId, int userId)
        {
            using (var dbContext = new DataContext())
            {
                var basket = dbContext.Baskets.Where(s => s.UserId == userId)
                    .First(s => s.ProductId == productId);
                dbContext.Baskets.Remove(basket);
                dbContext.SaveChanges();
            }
        }
    }
}