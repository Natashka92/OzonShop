using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public class StoreRepository
    {
        public static Store Find(int productId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Store.Find(productId);
            }
        }
    }
}