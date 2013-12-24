using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public class CurrencyRepository
    {
        public static Currency Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Currencies.Find(id);
            }
        }

        public static int ReturnId(int productId)
        {
            using (var dbContext = new DataContext())
            {
                Currency curr = dbContext.Products.Find(productId).Currency;
                return curr.CurrencyId;
            }
        }

        public static double Rate(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Currencies.Find(id).Rate;
            }
        }

        public static List<Currency> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Currencies.ToList();
            }
        }
    }
}