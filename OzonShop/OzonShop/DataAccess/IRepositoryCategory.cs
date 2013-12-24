using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataBase;

namespace OzonShop.DataAccess
{
    public interface IRepositoryCategory
    {
        List<Category> Select();
        List<int> SelectTreeCategory(int categoryId);
        bool IsParentCategory(int categoryId);
        List<Category> Select(int parentId);
        List<Category> Select(String searchString);
        void Delete(int id);
        Category Find(int id);
        void Edit(Category category);
        void Edit(int id, int idParent);
        void Add(Category category);
    }
}