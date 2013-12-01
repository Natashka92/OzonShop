using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Category")]
    public class Category : INamedEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public Category Parent { get; set; }

        public virtual ICollection<Product> Product { get; set; }

        public static List<Category> Select()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Categories.ToList();
            }
        }

        public static List<int> SelectTreeCategory(int categoryId)
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

        public static bool IsParentCategory(int categoryId)
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

        public static List<Category> Select(int parentId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Categories.Where(s => s.Parent.CategoryId == parentId).ToList();
            }
        }

        public static List<Category> Select(String searchString)
        {
            using (var dbContext = new DataContext())
            {
                var categories = Select();
                return categories = categories.Where(s => s.Name.Contains(searchString)).ToList();
            }
        }

        public static void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                Category category = dbContext.Categories.Find(id);
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
        }

        public static Category Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Categories.Find(id);
            }
        }

        public static void Edit(Category category)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Entry(category).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static void Edit(int id, int idParent)
        {
            using (var dbContext = new DataContext())
            {
                Category category = Find(id);
                category.Parent = Find(idParent);
                dbContext.Entry(category).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static void Add(Category category)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
            }
        }
    }
}
