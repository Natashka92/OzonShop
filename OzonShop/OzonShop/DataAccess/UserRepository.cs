using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.DataBase;
//using OzonShop.DataAccess;
using Shop;

namespace OzonShop.DataAccess
{
    public class UserRepository : IRepositoryUser//<User>
    {
        public List<User> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public List<User> Select(String searchString)
        {
            using (var dbContext = new DataContext())
            {
                var users = Select();
                return users = users.Where(s => s.Login.Contains(searchString)).ToList();
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                User user = dbContext.Users.Find(id);
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }

        public User Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Users.Find(id);
            }
        }

        public User Find(string Name)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Users.First(x => x.Login == Name);
            }
        }

        public List<String> SelectNamesByDescription(List<Description> comments)
        {
            using (var dbContext = new DataContext())
            {
                var userNames = new List<String>();                

                foreach (var item in comments)
                {
                    userNames.Add(dbContext.Users.Find(item.UserId).UserName);
                }
                return userNames;   
            }
        }
    }
}
