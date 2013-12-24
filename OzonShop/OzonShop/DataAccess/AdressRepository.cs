using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;
using System.Data.Entity;

namespace OzonShop.DataAccess
{
    public class AdressRepository
    {
        public static List<Adress> Select(int userId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Adresses.Where(s => s.UserId == userId).ToList();
            }
        }

        public static List<Adress> Select(string userName)
        {
            using (var dbContext = new DataContext())
            {
                User user = new UserRepository().Find(userName);
                return dbContext.Adresses.Where(s => s.UserId == user.UserId).ToList();
            }
        }

        public static Adress Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Adresses.Find(id);
            }
        }

        public static void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                var adress = dbContext.Adresses.Find(id);
                dbContext.Adresses.Remove(adress);
                dbContext.SaveChanges();
            }
        }

        public static void Edit(Adress adress)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Entry(adress).State = EntityState.Modified;
                dbContext.SaveChanges();
            }        
        }

        public static void Add(Adress adress)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Adresses.Add(adress);
                dbContext.SaveChanges();
            }
        }

        public static Adress FirstOrDefault(int userId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Adresses.FirstOrDefault(s => s.UserId == userId);                
            }
        }
    }
}