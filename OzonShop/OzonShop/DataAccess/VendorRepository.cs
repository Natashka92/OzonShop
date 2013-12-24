using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public class VendorRepository
    {
        public static Vendor Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Vendors.Find(id);
            }
        }
    }
}