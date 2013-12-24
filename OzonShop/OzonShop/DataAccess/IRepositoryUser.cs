using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public interface IRepositoryUser
    {
        void Delete(int id);
        List<User> Select();
        List<User> Select(String searchString);
        User Find(int id);
    }
}