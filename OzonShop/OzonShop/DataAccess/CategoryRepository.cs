using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop;
using Shop.DataBase;
using System.Data.Entity;

namespace OzonShop.DataAccess
{
    public class CategoryRepository : IRepositoryCategory
    {
        public List<Category> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Categories.ToList();
            }
        }

        public List<int> SelectTreeCategory(int categoryId)
        {
            var listCategory = new List<int>();
            listCategory.Add(categoryId);
            using (var dbContext = new DataContext())
            {
                var allCategory = Select(categoryId);
                foreach (var item in allCategory)
                {
                    listCategory.Add(item.CategoryId);
                }
                return listCategory;
            }
        }

        public bool IsParentCategory(int categoryId)
        {
            using (var dbContext = new DataContext())
            {
                var categories = dbContext.Categories;
                var headCategory = categories.Where(s => s.Parent == null).ToList();
                foreach (var item in headCategory)
                {
                    if (item.CategoryId == categoryId)
                        return true;
                }

                return false;
            }
        }

        public List<Category> Select(int parentId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Categories.Where(s => s.Parent.CategoryId == parentId).ToList();
            }
        }

        public List<Category> Select(String searchString)
        {
            using (var dbContext = new DataContext())
            {
                var categories = Select();
                return categories = categories.Where(s => s.Name.Contains(searchString)).ToList();
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                Category category = dbContext.Categories.Find(id);
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
        }

        public Category Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Categories.Find(id);
            }
        }

        public void Edit(Category category)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Entry(category).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void Edit(int id, int idParent)
        {
            using (var dbContext = new DataContext())
            {
                Category category = Find(id);
                category.Parent = Find(idParent);
                dbContext.Entry(category).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void Add(Category category)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
            }
        }
    }
}