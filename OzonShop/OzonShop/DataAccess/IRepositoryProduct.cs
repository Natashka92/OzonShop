using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public interface IRepositoryProduct
    {
        List<Product> Select();
        List<Product> Select(String searchString, int idTag, int categoryId);
        void Delete(int id);
        Product Find(int id);
        void Edit(Product product);
        void Add(Product product, String StringTags);
    }
}